﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace JVisserAssignment1
{
    public static class RandomNameGenerator
    {
        // User names form Docker Source Code https://github.com/docker/docker/blob/master/pkg/namesgenerator/names-generator.go
        private static List<string> left = new List<string>() { "admiring", "adoring", "affectionate", "agitated", "amazing", "angry", "awesome", "backstabbing", "berserk", "big", "boring", "clever", "cocky", "compassionate", "condescending", "cranky", "desperate", "determined", "distracted", "dreamy", "drunk", "eager", "ecstatic", "elastic", "elated", "elegant", "evil", "fervent", "focused", "furious", "gigantic", "gloomy", "goofy", "grave", "happy", "high", "hopeful", "hungry", "infallible", "jolly", "jovial", "kickass", "lonely", "loving", "mad", "modest", "naughty", "nauseous", "nostalgic", "peaceful", "pedantic", "pensive", "prickly", "reverent", "romantic", "sad", "serene", "sharp", "sick", "silly", "sleepy", "small", "stoic", "stupefied", "suspicious", "tender", "thirsty", "tiny", "trusting", "zen" };
        private static List<string> right = new List<string>() { "albattani", "allen", "almeida", "agnesi", "archimedes", "ardinghelli", "aryabhata", "austin", "babbage", "banach", "bardeen", "bartik", "bassi", "beaver", "bell", "bhabha", "bhaskara", "blackwell", "bohr", "booth", "borg", "bose", "boyd", "brahmagupta", "brattain", "brown", "carson", "chandrasekhar", "shannon", "clarke", "colden", "cori", "cray", "curran", "curie", "darwin", "davinci", "dijkstra", "dubinsky", "easley", "edison", "einstein", "elion", "engelbart", "euclid", "euler", "fermat", "fermi", "feynman", "franklin", "galileo", "gates", "goldberg", "goldstine", "goldwasser", "golick", "goodall", "haibt", "hamilton", "hawking", "heisenberg", "heyrovsky", "hodgkin", "hoover", "hopper", "hugle", "hypatia", "jang", "jennings", "jepsen", "joliot", "jones", "kalam", "kare", "keller", "khorana", "kilby", "kirch", "knuth", "kowalevski", "lalande", "lamarr", "lamport", "leakey", "leavitt", "lewin", "lichterman", "liskov", "lovelace", "lumiere", "mahavira", "mayer", "mccarthy", "mcclintock", "mclean", "mcnulty", "meitner", "meninsky", "mestorf", "minsky", "mirzakhani", "morse", "murdock", "newton", "nightingale", "nobel", "noether", "northcutt", "noyce", "panini", "pare", "pasteur", "payne", "perlman", "pike", "poincare", "poitras", "ptolemy", "raman", "ramanujan", "ride", "montalcini", "ritchie", "roentgen", "rosalind", "saha", "sammet", "shaw", "shirley", "shockley", "sinoussi", "snyder", "spence", "stallman", "stonebraker", "swanson", "swartz", "swirles", "tesla", "thompson", "torvalds", "turing", "varahamihira", "visvesvaraya", "volhard", "wescoff", "wiles", "williams", "wilson", "wing", "wozniak", "wright", "yalow", "yonath" };

        public static string GenerateName()
        {
            Thread.Sleep(10);
            Random rand = new Random((int)DateTime.Now.Ticks);
            
            return $"{left[rand.Next(left.Count)]} {right[rand.Next(right.Count)]}";
        }
    }
}
