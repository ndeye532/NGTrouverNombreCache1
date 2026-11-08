using System;
using System.Collections.Generic;

class Program
{
    static double meilleureNote = 0;

    static void Main()
    {
        while (true)
        {
            AfficherMessage("\n=== MENU PRINCIPAL ===");
            AfficherMessage("Choisissez 'F'-Facile 'D'-Difficile 'Q'-Quitter");
            string saisie = Console.ReadLine() ?? "";
            string choix = saisie.Trim().ToUpper();

            if (choix == "Q")
            {
                Console.Clear();
                AfficherMessage($"Programme arrêté. Meilleure note globale : {meilleureNote}%");
                break;
            }
            else if (choix == "F")
            {
                JouerModeFacile();
            }
            else if (choix == "D")
            {
                JouerModeDifficile();
            }
            else
            {
                AfficherMessage("Choix 'F', 'D' ou 'Q'");
            }
        }
    }

    static void AfficherMessage(string message)
    {
        Console.WriteLine(message);
    }

    static void JouerModeFacile()
    {
        Random rnd = new Random();
        int nombreCache = rnd.Next(1, 11);
        List<int> essaisPrecedents = new List<int>();
        int nbEssais = 0;

        while (true)
        {
            AfficherMessage("\nChoisissez un nombre entre [1, 10] ou 'Q'-Quitter");
            string saisie = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (saisie == "Q")
            {
                Console.Clear();
                AfficherMessage($"Meilleure note du joueur : {meilleureNote}%");
                return;
            }

            if (int.TryParse(saisie, out int choixNombre) && choixNombre >= 1 && choixNombre <= 10)
            {
                nbEssais++;
                essaisPrecedents.Add(choixNombre);

                if (choixNombre == nombreCache)
                {
                    Console.Clear();
                    AfficherMessage("vous avez gagné");
                    double noteActuelle = ((10.0 - 1.0 + 1.0 - nbEssais) / (10.0 - 1.0 + 1.0)) * 100.0;
                    if (noteActuelle > meilleureNote) meilleureNote = noteActuelle;
                    AfficherMessage($"Note : {noteActuelle}% | Meilleure note : {meilleureNote}%");
                    nombreCache = rnd.Next(1, 11);
                    essaisPrecedents.Clear();
                    nbEssais = 0;
                }
                else
                {
                    AfficherMessage("vous avez perdu");
                    AfficherMessage("Réponses déjà données : " + string.Join(", ", essaisPrecedents));
                }
            }
            else
            {
                AfficherMessage("réponse doit être comprise entre 1 et 10");
            }
        }
    }

    static void JouerModeDifficile()
    {
        int min = 0, max = 0;

        while (true)
        {
            AfficherMessage("\nEntrez les bornes min et max de l'intervalle, ou 'Q'-Quitter");
            string saisie = Console.ReadLine()?.Trim() ?? "";

            if (saisie.ToUpper() == "Q")
            {
                Console.Clear();
                AfficherMessage($"Meilleure note du joueur : {meilleureNote}%");
                return;
            }

            string[] parties = saisie.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parties.Length == 2 && int.TryParse(parties[0], out min) && int.TryParse(parties[1], out max))
            {
                if (min < max && (max - min > 10))
                {
                    break;
                }
            }
            AfficherMessage("Choisissez 2 nombres entiers et (max - min) doit être > à 10");
        }

        Random rnd = new Random();
        int nombreCache = rnd.Next(min, max + 1);
        int nbEssais = 0;
        int etendue = max - min + 1;

        while (true)
        {
            AfficherMessage($"choisissez un nombre entre [{min}, {max}] ou 'Q'-Quitter");
            string saisie = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (saisie == "Q")
            {
                Console.Clear();
                return;
            }

            if (int.TryParse(saisie, out int choixNombre) && choixNombre >= min && choixNombre <= max)
            {
                nbEssais++;

                if (choixNombre == nombreCache)
                {
                    Console.Clear();
                    AfficherMessage("vous avez gagné");
                    double noteActuelle = ((double)(etendue - nbEssais) / etendue) * 100.0;
                    if (noteActuelle < 0) noteActuelle = 0;
                    if (noteActuelle > meilleureNote) meilleureNote = noteActuelle;
                    AfficherMessage($"Note : {noteActuelle:F2}% | Meilleure note : {meilleureNote:F2}%");
                    JouerModeDifficile();
                    return;
                }
                else
                {
                    AfficherMessage("vous avez perdu");
                }
            }
            else
            {
                AfficherMessage("Choix doit être compris entre Min et Max");
            }
        }
    }
}