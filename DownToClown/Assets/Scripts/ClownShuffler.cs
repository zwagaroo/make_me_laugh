using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/*public class int
{
    public string name;
}


public class int
{
    public string name;
}
*/


[Serializable]
public class Round
{
    public Dictionary<int, int> roles = new();

    public List<int> GetUsedCharacters()
    {
        return roles.Values.ToList();
    }

    public static List<int> GetPreviousCharacters(List<Round> rounds, int player, int currentRound)
    {
        List<int> previousCharacters = new List<int>();
        for (int round = 0; round < currentRound; round++)
        {
            previousCharacters.Add(rounds[round].roles[player]);
        }
        return previousCharacters;
    }

    public static List<int> GetInvalidCharacters(List<Round> rounds, int player, int currentRound)
    {
        List<int> previousCharacters = GetPreviousCharacters(rounds, player, currentRound);
        return rounds[currentRound].GetUsedCharacters().Union(previousCharacters).ToList();
    }

    public static List<int> GetValidCharacters(List<Round> rounds, int player, int currentRound, List<int> characters)
    {
        return characters.Except(GetInvalidCharacters(rounds, player, currentRound)).ToList();
    }

    public static int PickRandomCharacter(List<Round> rounds, int player, int currentRound, List<int> characters)
    {
        List<int> validCharacters = GetValidCharacters(rounds, player, currentRound, characters);
        if (validCharacters.Count == 0)
        {
            Debug.LogError("NO VALID CHARACTER AVAILIABLE");
        }
        int randomIndex = UnityEngine.Random.Range(0, validCharacters.Count);
        return validCharacters[randomIndex];
    }
}

public class ClownShuffler : MonoBehaviour
{
    public static List<Round> rounds;

    public static int currentRound = 0;

    public static void SetNamesAndClowns(List<int> players, List<int> characters)
    {
        currentRound = 0;
        rounds = new List<Round>();

        for (int i = 0; i < players.Count; i++)
        {
            rounds.Add(new Round());
        }

        foreach (var player in players)
        {
            for (int round = 0; round < players.Count; round++)
            {
                rounds[round].roles[player] = Round.PickRandomCharacter(rounds, player, round, characters);
            }
        }
    }
/*
    public void Start()
    {
        // Create some sample players and characters
        List<int> players = new List<int>
        {
            new int { name = "int1" },
            new int { name = "int2" },
            new int { name = "int3" },
            new int { name = "int4" }
        };

        List<int> characters = new List<int>
        {
            new int { name = "Character1" },
            new int { name = "Character2" },
            new int { name = "Character3" },
            new int { name = "Character4" }
        };

        // Set names and clowns
        SetNamesAndClowns(players, characters);

        // Print out the mappings for each round
        for (int round = 0; round < rounds.Count; round++)
        {
            Debug.Log("Round " + (round + 1) + " mappings:");
            foreach (var kvp in rounds[round].roles)
            {
                Debug.Log(kvp.Key.name + " - " + kvp.Value.name);
            }
        }
    }*/
}