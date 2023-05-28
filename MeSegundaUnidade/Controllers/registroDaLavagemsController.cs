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
    public class registroDaLavagemsController : Controller
    {
        private dgmsvi72o1j1fpnEntities db = new dgmsvi72o1j1fpnEntities();

        // GET: registroDaLavagems
        public ActionResult Index()
        {
            var registroDaLavagems = db.registroDaLavagems.Include(r => r.funcionario).Include(r => r.veiculo);
            return View(registroDaLavagems.ToList());
        }

        // GET: registroDaLavagems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registroDaLavagem registroDaLavagem = db.registroDaLavagems.Find(id);
            if (registroDaLavagem == null)
            {
                return HttpNotFound();
            }
            return View(registroDaLavagem);
        }

        // GET: registroDaLavagems/Relatorio
        public ActionResult Relatorio(DateTime? startDate, DateTime? endDate, Guid? funcionarioId)
        {
            var registroDaLavagem = db.registroDaLavagems.AsQueryable();
            ViewBag.Funcionarios = new SelectList(db.funcionarios, "id", "nome");

            if (startDate.HasValue)
            {
                var startDateTemp = startDate.Value.AddDays(-1);
                registroDaLavagem = registroDaLavagem.Where(r => r.datadalavagem >= startDateTemp);
            }

            if (endDate.HasValue)
            {
                var endDateTemp = endDate.Value.AddDays(1);
                registroDaLavagem = registroDaLavagem.Where(r => r.datadalavagem <= endDateTemp);
            }

            if (funcionarioId.HasValue)
            {
                registroDaLavagem = registroDaLavagem.Where(r => r.idfuncionario == funcionarioId.Value);
            }

            if (registroDaLavagem == null)
            {
                return HttpNotFound();
            }

            return View(registroDaLavagem);
        }

        // GET: registroDaLavagems/Create
        public ActionResult Create()
        {
            ViewBag.idfuncionario = new SelectList(db.funcionarios.Where(x => x.Disponivel == true), "id", "nome");
            ViewBag.idveiculo = new SelectList(db.veiculoes.Where(x => x.EmLavagem == false), "id", "placa");
            return View();
        }

        // POST: registroDaLavagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cpfDoFuncionario,datadalavagem,hora,tipo,valorcobrado,idfuncionario,idveiculo")] registroDaLavagem registroDaLavagem)
        {
            var funcionario = db.funcionarios.FirstOrDefault(x => x.id.ToString().Equals(registroDaLavagem.idfuncionario.ToString()));
            var veiculo = db.veiculoes.FirstOrDefault(x => x.id.ToString().Equals(registroDaLavagem.idveiculo.ToString()));

            ViewBag.idfuncionario = new SelectList(db.funcionarios.Where(x => x.Disponivel == true), "id", "nome");
            ViewBag.idveiculo = new SelectList(db.veiculoes.Where(x => x.EmLavagem == false), "id", "placa");

            if (funcionario.cpf != registroDaLavagem.cpfDoFuncionario)
            {
                string mensagem = "O cpf informado nao pertence ao funcionario selecionado!";
                ViewBag.Erro = mensagem;
                return View(registroDaLavagem);
            }

            if (veiculo.EmLavagem.Value)
            {
                string mensagem = "O veiculo informado se encontra em uma lavagem!";
                ViewBag.Erro = mensagem;
                return View(registroDaLavagem);
            }

            var existeLavagem = db.registroDaLavagems.FirstOrDefault(x => x.idfuncionario == funcionario.id && x.datadalavagem == registroDaLavagem.datadalavagem);

            if (existeLavagem != null)
            {
                if (existeLavagem.datadalavagem.Value == registroDaLavagem.datadalavagem)
                {
                    string mensagem = "Existe uma lavagem para o agendada para o funcionario nessa data e hora informada";
                    ViewBag.Erro = mensagem;
                    return View(registroDaLavagem);
                }
            }

            if (ModelState.IsValid)
            {
                registroDaLavagem.id = Guid.NewGuid();
                db.registroDaLavagems.Add(registroDaLavagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idfuncionario = new SelectList(db.funcionarios, "id", "nome", registroDaLavagem.idfuncionario);
            ViewBag.idveiculo = new SelectList(db.veiculoes, "id", "placa", registroDaLavagem.idveiculo);
            return View(registroDaLavagem);
        }

        // GET: registroDaLavagems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registroDaLavagem registroDaLavagem = db.registroDaLavagems.Find(id);
            if (registroDaLavagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.idfuncionario = new SelectList(db.funcionarios, "id", "nome", registroDaLavagem.idfuncionario);
            ViewBag.idveiculo = new SelectList(db.veiculoes, "id", "placa", registroDaLavagem.idveiculo);
            return View(registroDaLavagem);
        }

        // POST: registroDaLavagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cpfDoFuncionario,datadalavagem,hora,tipo,valorcobrado,idfuncionario,idveiculo")] registroDaLavagem registroDaLavagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroDaLavagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idfuncionario = new SelectList(db.funcionarios, "id", "nome", registroDaLavagem.idfuncionario);
            ViewBag.idveiculo = new SelectList(db.veiculoes, "id", "placa", registroDaLavagem.idveiculo);
            return View(registroDaLavagem);
        }

        // GET: registroDaLavagems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registroDaLavagem registroDaLavagem = db.registroDaLavagems.Find(id);
            if (registroDaLavagem == null)
            {
                return HttpNotFound();
            }
            return View(registroDaLavagem);
        }

        // POST: registroDaLavagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            registroDaLavagem registroDaLavagem = db.registroDaLavagems.Find(id);
            db.registroDaLavagems.Remove(registroDaLavagem);
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
