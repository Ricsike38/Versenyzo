using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Versenyzo
{
    class Versenyzo
    {
        // mezők
        private int rajtSzam;
        private string nev;
        private string szak;

        private int pontSzam;
        //konstruktor
        public Versenyzo(int rajtSzam, string nev, string szak)
        {
            this.rajtSzam = rajtSzam;
            this.nev = nev;
            this.szak = szak;

        }

        //metódusok
        public void PontotKap(int pont)
        {
            pontSzam += pont;

        }

        public override string ToString()
        {
            return rajtSzam + "\t" + nev + "\t" + szak + "\t" + pontSzam + "pont";
        }

        //tulajdonságok
        public int RajtSzam
        {
            get { return rajtSzam; }
        }
        public string Szak
        {
            get { return szak; }
        }
        public int PontSzam
        {
            get { return pontSzam; }
        }

   
       
        internal class Program
        {


            class VezerloOsztaly { 
        public void Start()
        {
            AdatBevitel();

            Kiiratas("\n Résztvevők: \n");
            Verseny();
            Kiiratas("\nEredmények:\n");

            Eredmenyek();
            Keresesek();
        }
            
            private void AdatBevitel()
            {
                Versenyzo versenyzo;
                string nev, szak;
                int sorszam = 1;

                StreamReader olvasoCsatorna = new StreamReader("C:/Most/nagyárpi.txt");

                while (!olvasoCsatorna.EndOfStream)
                {
                    nev = olvasoCsatorna.ReadLine();
                    szak = olvasoCsatorna.ReadLine();

                    versenyzo = new Versenyzo(sorszam, nev, szak);

                    versenyzok.Add(versenyzo);

                    sorszam++;
                }
                olvasoCsatorna.Close();
            }

            private void Kiiratas(string cim)
            {
                Console.WriteLine(cim);
                foreach (Versenyzo enekes in versenyzok)
                {
                    Console.WriteLine(enekes);
                }

            }
            private int zsuriLetszam = 5;
            private int pontHatar = 10;

            private void Verseny()
            {

                Random rand = new Random();
                int pont;
                foreach (Versenyzo versenyzo in versenyzok)
                {
                    for (int i = 0; i < zsuriLetszam; i++)
                    {
                        pont = rand.Next(pontHatar);
                        versenyzo.PontotKap(pont);
                    }
                }
            }
            private void Eredmenyek()
            {
                int max = versenyzok[0].pontSzam;

                foreach (Versenyzo enekes in versenyzok)
                {
                    if (enekes.PontSzam > max)
                    {
                        max = enekes.PontSzam;

                    }
                }
                Console.WriteLine("\n A legjobb(ak) \n");
                foreach (Versenyzo enekes in versenyzok)
                {
                    if (enekes.PontSzam == max)
                    {
                        Console.WriteLine(enekes);
                    }
                }
            }

            private void Sorrend()
            {
                Versenyzo temp;
                for (int i = 0; i < versenyzok.Count - 1; i++)
                {
                    for (int j = 0; j < versenyzok.Count; j++)
                    {
                        if (versenyzok[i].pontSzam < versenyzok[j].PontSzam)
                        {
                            temp = versenyzok[i];
                            versenyzok[i] = versenyzok[j];
                            versenyzok[j] = temp;
                        }
                    }
                }
                Kiiratas("\nEredménytábla\n");
            }
            private void Keresesek()
            {
                Console.WriteLine("\nAdott a szakhoz tartozó énekesek keresése\n");
                Console.WriteLine("\nKeres valakit? (i/n)");

                char valasz;
                while (!char.TryParse(Console.ReadLine(), out valasz))
                {
                    Console.WriteLine("Egy karaktert írjon.");

                }
                string szak;
                bool vanIlyen;

                while (valasz == 'i' || valasz == 'I')
                {
                    Console.Write("Szak: ");
                    szak = Console.ReadLine();
                    vanIlyen = false;

                    foreach (Versenyzo enekes in versenyzok)
                    {
                        if (enekes.Szak == szak)
                        {
                            Console.WriteLine(enekes);
                            vanIlyen = true;
                        }
                    }
                    if (!vanIlyen)
                    {
                        Console.WriteLine("Erről a szakról senki sem indult.");
                    }
                    Console.Write("\n Keres még valakit? (i/n)");
                    valasz = char.Parse(Console.ReadLine());
                }
            }
            private List<Versenyzo> versenyzok = new List<Versenyzo>();
            static void Main(string[] args)
            {


                new VezerloOsztaly().Start();
                
                Console.ReadKey();


            }
        }
    }
}
}
