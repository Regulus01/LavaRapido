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
    public class veiculoController : Controller
    {
        private dgmsvi72o1j1fpnEntities db = new dgmsvi72o1j1fpnEntities();

        // GET: veiculo
        public ActionResult Index()
        {
            return View(db.veiculoes.ToList());
        }

        // GET: veiculo/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            veiculo veiculo = db.veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // GET: veiculo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: veiculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,placa,marca,modelo,ano")] veiculo veiculo)
        {
            var placaExiste = db.veiculoes.FirstOrDefault(x => x.placa.Equals(veiculo.placa.Trim()));

            if (placaExiste != null)
            {
                string mensagem = $"A placa {veiculo.placa} esta cadastrada";
                ViewBag.Erro = mensagem;
                return View(veiculo);
            }

            if (ModelState.IsValid)
            {
                veiculo.id = Guid.NewGuid();
                veiculo.EmLavagem = false;
                db.veiculoes.Add(veiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veiculo);
        }

        // GET: veiculo/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            veiculo veiculo = db.veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: veiculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,placa,marca,modelo,ano")] veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }

        // GET: veiculo/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            veiculo veiculo = db.veiculoes.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: veiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            veiculo veiculo = db.veiculoes.Find(id);
            db.veiculoes.Remove(veiculo);
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
