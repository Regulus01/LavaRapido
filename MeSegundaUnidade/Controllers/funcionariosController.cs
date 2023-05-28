using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeSegundaUnidade;

namespace MeSegundaUnidade.Controllers
{
    public class funcionariosController : Controller
    {
        private dgmsvi72o1j1fpnEntities db = new dgmsvi72o1j1fpnEntities();

        // GET: funcionarios
        public ActionResult Index()
        {
            return View(db.funcionarios.ToList());
        }

        // GET: funcionarios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionario funcionario = db.funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,datadenascimento,cpf,endereco")] funcionario funcionario)
        {
            var funcionarioExiste = db.funcionarios.FirstOrDefault(x => x.cpf.Equals(funcionario.cpf.Trim()));

            if (funcionarioExiste != null)
            {
                string mensagem = "O cpf informado ja esta cadastrado!";
                ViewBag.Erro = mensagem;
                return View(funcionario);
            }

            if (ModelState.IsValid)
            {
                funcionario.Disponivel = true;
                funcionario.id = Guid.NewGuid();
                db.funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: funcionarios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionario funcionario = db.funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,datadenascimento,cpf,endereco")] funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: funcionarios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionario funcionario = db.funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            funcionario funcionario = db.funcionarios.Find(id);
            db.funcionarios.Remove(funcionario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
