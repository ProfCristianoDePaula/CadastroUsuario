﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroUsuario.Data;
using CadastroUsuario.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace CadastroUsuario.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuariosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // Verificar o usuario que esta autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                ModelState.AddModelError("Loggin", "Usuário não autenticado.");
                return RedirectToAction("Index", "Home");
            }

            // Filtar os usuarios que tem o mesmo TipoUsuario do usuario autenticado
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.AppUserId == Guid.Parse(userId));

            // Retornar a lista com os usuarios que tem o mesmo TipoUsuario do usuario autenticado
            var usuarios = await _context.Usuarios
                .Where(u => u.TipoUsuario == usuario.TipoUsuario)
                .ToListAsync();

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,NomeCompleto,Celular,CPF,AppUserId,TipoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Setar o AppUserId com o Id do Usuario do Identity
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    ModelState.AddModelError("", "Usuário não autenticado.");
                    return View(usuario);
                }

                // Verifica se o usuário já existe
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.AppUserId == Guid.Parse(userId));

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("", "Usuário já cadastrado.");
                    return View(usuario);
                }

                // Definir o AppUserId com o Id do usuario autenticado
                usuario.AppUserId = Guid.Parse(userId);

                // Fazer o vinculo do IdentityUser com o Usuario
                var identityUser = await _context.Users.FindAsync(userId);
                usuario.IdentityUser = identityUser;

                //// Criar um registro no AspNetUsersRoles com o Tipo de Usuário
                //var role = new IdentityUserRole<string>
                //{
                //    UserId = userId,
                //    RoleId = _context.Roles.Where(r => r.Name == usuario.TipoUsuario).First().Id // Defina o ID do papel desejado aqui
                //};

                // Adiciona a role ao usuário
                await _userManager.AddToRoleAsync(identityUser, usuario.TipoUsuario);

                // Atualiza o cookie de autenticação
                await _signInManager.RefreshSignInAsync(identityUser);

                usuario.UsuarioId = Guid.NewGuid();
                _context.Add(usuario);

                //_context.UserRoles.Add(role);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UsuarioId,NomeCompleto,Celular,CPF,AppUserId,TipoUsuario")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
