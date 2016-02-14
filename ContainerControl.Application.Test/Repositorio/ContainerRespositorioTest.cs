using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerControl.Application.Repository;
using ContainerControl.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerControl.Application.Test.Repositorio
{
    [TestClass]
    public class ContainerRespositorioTest
    {
        private string CodePredicate => "CTNR";
        private int CodeSeqBorder => 99999;


        private string GenerateRandomCode()
        {
            Random nmbr = new Random();
            return $"{CodePredicate}_{nmbr.Next(CodeSeqBorder)}";
        }

        private List<Container> Inserted { get; set; }

        private int GetRandomIndex(IList list)
        {
            Random nmbr = new Random();
            return nmbr.Next(0, list.Count);
        }

        private double GetRandom(int max)
        {
            Random rnd = new Random();
            return rnd.NextDouble() * rnd.Next(max);
        }

        private void GenerateCodigosIso()
        {
            List<CodigoIso> cods = new List<CodigoIso>();
            for (int i = 0; i < 5; i++)
            {
                string cod = GenerateRandomCode();
                while (cods.Any(c => c.Codigo == cod))
                {
                    cod = GenerateRandomCode();
                }

                CodigoIso codigoIso = new CodigoIso()
                {
                    Codigo = cod,
                    Nome = cod
                };

                using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
                {
                    cods.Add(repo.Inserir(codigoIso));
                }
            }
        }

        private void ClearCodigosIso()
        {
            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                foreach (CodigoIso cod in repo.Listar())
                {
                    repo.Excluir(cod);
                }
            }

            Inserted.Clear();
        }

        [TestInitialize]
        public void InitializeDb()
        {
            GenerateCodigosIso();
            Inserted = new List<Container>();
            for (int i = 0; i < 5; i++)
            {
                string cod = GenerateRandomCode();
                while (Inserted.Any(c => c.NroContainer == cod))
                {
                    cod = GenerateRandomCode();
                }

                double cm = GetRandom(99999);
                double peso = GetRandom((int)cm);
                double tara = GetRandom(1000);
                CodigoIso codIso = null;

                using (CodigoIsoRepositorio cirepo = new CodigoIsoRepositorio())
                {
                    codIso = cirepo.Listar().ToList()[GetRandomIndex(cirepo.Listar().ToList())];
                }

                Container ctnr = new Container()
                {
                    NroContainer = cod,
                    CM = cm,
                    CodigoIso = codIso,
                    Fabricacao = new DateTime(2012, 12, 12),
                    Peso = peso,
                    Tara = tara
                };

                using (ContainerRepositorio repo = new ContainerRepositorio())
                {
                    Inserted.Add(repo.Inserir(ctnr));
                }
            }
        }

        //[TestCleanup]
        public void ClearDb()
        {
            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                foreach (Container ctnr in repo.Listar())
                {
                    repo.Excluir(ctnr);
                }
            }

            ClearCodigosIso();

            Inserted.Clear();
        }

        [TestMethod]
        public void Query()
        {
            List<Container> lista = null;
            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                lista = repo.Listar().ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void GetById()
        {
            Container cod = null;
            int index = GetRandomIndex(Inserted);
            var item = Inserted[index];
            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                cod = repo.CapturarPorId(item.Id);
            }

            Assert.IsNotNull(cod);
            Assert.IsInstanceOfType(cod, typeof(Container));
            Assert.AreEqual(cod.Id, item.Id);
        }

        [TestMethod]
        public void GetOneBy()
        {
            List<Container> lista = null;

            int index = GetRandomIndex(Inserted);
            var item = Inserted[index];

            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                lista = repo.CapturarPor(c => c.NroContainer == item.NroContainer).ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreEqual(lista.Count, 1);
            Assert.AreEqual(lista[0].NroContainer, item.NroContainer);
            Assert.AreEqual(lista[0].Id, item.Id);
        }

        [TestMethod]
        public void GetListBy()
        {
            List<Container> lista = null;
            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                lista = repo.CapturarPor(c => c.NroContainer.Contains(CodePredicate)).ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void Insert()
        {
            string cod = GenerateRandomCode();
            while (Inserted.Any(c => c.NroContainer == cod))
            {
                cod = GenerateRandomCode();
            }

            double cm = GetRandom(99999);
            double peso = GetRandom((int)cm);
            double tara = GetRandom(1000);
            CodigoIso codIso = null;

            using (CodigoIsoRepositorio cirepo = new CodigoIsoRepositorio())
            {
                codIso = cirepo.Listar().ToList()[GetRandomIndex(cirepo.Listar().ToList())];
            }

            Container ctnr = new Container()
            {
                NroContainer = cod,
                CM = cm,
                CodigoIso = codIso,
                Fabricacao = new DateTime(2012, 12, 12),
                Peso = peso,
                Tara = tara
            };

            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                ctnr = repo.InserirOuAtualizar(ctnr);
                Inserted.Add(ctnr);
            }

            Assert.IsNotNull(ctnr);
            Assert.AreNotEqual(ctnr.Id, Guid.Empty);
        }

        [TestMethod]
        public void Update()
        {
            double cm = GetRandom(99999);
            double peso = GetRandom((int)cm);
            double tara = GetRandom(1000);
            Container ctnr = null;
            string ctnrNr = string.Empty;

            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                ctnr = repo.Listar().FirstOrDefault();

                if (ctnr != null)
                {
                    ctnrNr = ctnr.NroContainer;

                    ctnr.CM = cm;
                    ctnr.Peso = peso;
                    ctnr.Tara = tara;

                    ctnr = repo.InserirOuAtualizar(ctnr);
                }
            }

            Assert.IsNotNull(ctnr);
            Assert.AreNotEqual(ctnr.Id, Guid.Empty);
            
            Assert.AreEqual(ctnr.NroContainer, ctnrNr);
            Assert.AreEqual(ctnr.CM, cm);
            Assert.AreEqual(ctnr.Peso, peso);
            Assert.AreEqual(ctnr.Tara, tara);
        }

        [TestMethod]
        public void DeleteById()
        {
            int countOld = 0;
            int countNew = 0;
            Container Container = null;
            int index = GetRandomIndex(Inserted);
            var item = Inserted[index];

            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                countOld = repo.Listar().Count();
                Container = repo.Excluir(item.Id);
                countNew = repo.Listar().Count();

                Container = repo.CapturarPorId(item.Id);
            }

            Assert.IsNull(Container);
            Assert.AreNotSame(countOld, countNew);
            Assert.IsTrue(Math.Abs(countOld - countNew) == 1);
        }

        [TestMethod]
        public void Delete()
        {
            int countOld = 0;
            int countNew = 0;
            Container Container = null;

            int index = GetRandomIndex(Inserted);
            var item = Inserted[index];

            using (ContainerRepositorio repo = new ContainerRepositorio())
            {
                countOld = repo.Listar().Count();
                Container = repo.Excluir(item);
                countNew = repo.Listar().Count();

                Container = repo.CapturarPorId(item.Id);
            }

            Assert.IsNull(Container);
            Assert.AreNotSame(countOld, countNew);
            Assert.IsTrue(Math.Abs(countOld - countNew) == 1);
        }
    }
}
