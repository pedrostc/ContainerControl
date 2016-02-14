using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ContainerControl.Application.Repository;
using ContainerControl.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerControl.Application.Test.Repositorio
{
    [TestClass]
    public class CodigoIsoRepositorioTest
    {
        private string CodePredicate => "COD";
        private int CodeSeqBorder => 9999999;


        private string GenerateRandomCode()
        {
            Random nmbr = new Random();
            return $"{CodePredicate}_{nmbr.Next(CodeSeqBorder)}";
        }

        private List<CodigoIso> Inserted { get; set; }

        private int GetRandomIndex(List<CodigoIso> list)
        {
            Random nmbr = new Random();
            return nmbr.Next(0, list.Count);
        }

        [TestInitialize]
        public void InitializeDb()
        {
            Inserted = new List<CodigoIso>();
            for (int i = 0; i < 5; i++)
            {
                string cod = GenerateRandomCode();
                while (Inserted.Any(c => c.Codigo == cod))
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
                    Inserted.Add(repo.Inserir(codigoIso));
                }
            }
        }

        [TestCleanup]
        public void ClearDb()
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

        [TestMethod]
        public void Insert()
        {
            for (int i = 0; i < 5; i++)
            {
                string cod = GenerateRandomCode();
                while (Inserted.Any(c => c.Codigo == cod))
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
                    codigoIso = repo.Inserir(codigoIso);
                    Inserted.Add(codigoIso);
                }
                
                Assert.IsNotNull(codigoIso);
                Assert.AreNotEqual(codigoIso.Id, Guid.Empty);
                Assert.AreEqual(codigoIso.Codigo, cod);
                Assert.AreNotEqual(codigoIso.CriadoEm, default(DateTime));
                Assert.AreNotEqual(codigoIso.ModificadoEm, default(DateTime));
            }
        }

        [TestMethod]
        public void Query()
        {
            List<CodigoIso> lista = null;
            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                lista = repo.Listar().ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void GetById()
        {
            CodigoIso cod = null;
            int index = GetRandomIndex(Inserted);

            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                cod = repo.CapturarPorId(Inserted[index].Id);
            }

            Assert.IsNotNull(cod);
            Assert.IsInstanceOfType(cod, typeof(CodigoIso));
            Assert.AreEqual(cod.Id, Inserted[index].Id);
        }

        [TestMethod]
        public void GetOneBy()
        {
            List<CodigoIso> lista = null;
            int index = GetRandomIndex(Inserted);
            CodigoIso item = Inserted[index];

            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                lista = repo.CapturarPor(c => c.Codigo == item.Codigo).ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreEqual(lista.Count, 1);
            Assert.AreEqual(lista[0].Codigo, Inserted[index].Codigo);
            Assert.AreEqual(lista[0].Id, Inserted[index].Id);
        }

        [TestMethod]
        public void GetListBy()
        {
            List<CodigoIso> lista = null;
            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                lista = repo.CapturarPor(c => c.Codigo.Contains("COD_")).ToList();
            }

            Assert.IsNotNull(lista);
            Assert.AreNotEqual(lista.Count, 0);
        }

        [TestMethod]
        public void Update()
        {
            Random nmbr = new Random();
            string cod = GenerateRandomCode();
            while (Inserted.Any(c => c.Codigo == cod))
            {
                cod = GenerateRandomCode();
            }

            CodigoIso codigoIso = null;
            int index = GetRandomIndex(Inserted);

            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                codigoIso = repo.CapturarPorId(Inserted[index].Id);
                codigoIso.Nome = cod;
                codigoIso.Codigo = cod;

                codigoIso = repo.InserirOuAtualizar(codigoIso);
            }

            Assert.IsNotNull(codigoIso);
            Assert.AreNotEqual(codigoIso.Id, Guid.Empty);
            Assert.AreEqual(codigoIso.Id, Inserted[index].Id);
            Assert.AreEqual(codigoIso.Codigo, cod);
        }

        [TestMethod]
        public void DeleteById()
        {
            int countOld = 0;
            int countNew = 0;
            CodigoIso codigoIso = null;
            Guid id = Guid.Empty;

            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                countOld = repo.Listar().Count();

                codigoIso = repo.Listar().FirstOrDefault();
                id = codigoIso.Id;

                codigoIso = repo.Excluir(id);
                countNew = repo.Listar().Count();

                codigoIso = repo.CapturarPorId(id);
            }

            Assert.IsNull(codigoIso);
            Assert.AreNotSame(countOld, countNew);
            Assert.IsTrue(Math.Abs(countOld - countNew) == 1);
        }

        [TestMethod]
        public void Delete()
        {
            int countOld = 0;
            int countNew = 0;
            CodigoIso codigoIso = null;
            Guid id = Guid.Empty;

            using (CodigoIsoRepositorio repo = new CodigoIsoRepositorio())
            {
                countOld = repo.Listar().Count();

                codigoIso = repo.Listar().FirstOrDefault();
                id = codigoIso.Id;

                codigoIso = repo.Excluir(codigoIso);
                countNew = repo.Listar().Count();

                codigoIso = repo.CapturarPorId(id);
            }

            Assert.IsNull(codigoIso);
            Assert.AreNotSame(countOld, countNew);
            Assert.IsTrue(Math.Abs(countOld - countNew) == 1);
        }
    }
}
