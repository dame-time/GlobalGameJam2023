using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public sealed class RandomStringGenerator : MonoBehaviour
    {
        // private static string[] characters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static string[] characters = new string[] { "A", "B", "C"};

        public static string GenerateRandomString(int length)
        {
            var randomString = "*" + characters[Random.Range(0, characters.Length)];

            for (int i = 0; i < length - 1; i++)
            {
                int randomRange = Random.Range(-1, 2);

                var lastChar = randomString.ToCharArray().ToList()[randomString.Length - 1];
                var randomCharIndex = Mathf.Abs(characters.ToList().FindIndex(e => e == lastChar.ToString()) + randomRange) % characters.Length;

                randomString += characters[randomCharIndex];
            }

            return randomString;
        }
    }
}
