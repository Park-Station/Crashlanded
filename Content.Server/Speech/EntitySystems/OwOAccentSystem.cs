using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems
{
    public sealed class OwOAccentSystem : EntitySystem
    {
        [Dependency] private readonly IRobustRandom _random = default!;

        private IReadOnlyList<string> Prefixes = new List<string>
        {
            "<3", "HIII!", "Haiiii,", "H-hewwo?", "(#o.o)", ";;w;;", ";w;", "Weh!"
        };
        private IReadOnlyList<string> nPrefixes = new List<string>
        {
            "HIII!", "Haiiii,", "Hewwo?", "Weh!"
        };

        private IReadOnlyList<string> Faces = new List<string>
        {
            "(·`ω´·)", ";;w;;", ";w;", "OwO", "UwU", ">w<", "^w^", "TwT", "-w-", "(^U^)", "✪ω✪", "(^▽^)", "(^///^)", "x3"
        };

        private IReadOnlyList<string> CFaces = new List<string>
        {
            ">_<", "(^///^)", "(._. )", "(⊙_⊙#)", "x3", ":3", ";3", ";-;"
        };

        private IReadOnlyList<string> Suffixes = new List<string>
        {
            "Ɛ>", "baii!", "bye bye!", "ceeya! :D", "weh!"
        };
        private IReadOnlyList<string> nSuffixes = new List<string>
        {
            "baii!", "bye bye!", "ceeya!", "weh!"
        };

        private IReadOnlyDictionary<string, string> SpecialWords = new Dictionary<string, string>()
        {
            // TODO: Case insensitive
            { "FUCK", "WUH OH" },
            { "Fuck", "Wuh oh" },
            { "fuck", "wuh oh" },
            { "SHIT", "CRAP" },
            { "Shit", "Crap" },
            { "shit", "crap" },

            { "YOU", "WU" },
            { "You", "Wu" },
            { "you", "wu" },

            { "NO", "NU" },
            { "No", "Nu" },
            { "no", "nu" },

            { "HAS", "HAZ" },
            { "Has", "Haz" },
            { "has", "haz" },
            { "SAYS", "SEZ" },
            { "Says", "Sez" },
            { "says", "sez" },
            { "WAS", "WUZ" },
            { "Was", "Wuz" },
            { "was", "wuz" },

            { "THERE", "DERE" },
            { "There", "Dere" },
            { "there", "dere" },
            { "THEN", "DEN" },
            { "Then", "Den" },
            { "then", "den" },
            { "THE", "DA" },
            { "The", "Da" },
            { "the", "da" },

            { "HUGGED", "CUDDLED" },
            { "Hugged", "Cuddled" },
            { "hugged", "cuddled" },
            { "HUGGING", "CUDDLING" },
            { "Hugging", "Cuddling" },
            { "hugging", "cuddling" },
            { "HUG", "CUDDLE" },
            { "Hug", "Cuddle" },
            { "hug", "cuddle" },

            { "SLEEP", "NIGHT NIGHT"},
            { "Sleep", "Night night"},
            { "sleep", "night night"},
            { "DYING", "SLEEPY" },
            { "Dying", "Sleepy" },
            { "dying", "sleepy" },
            { "DEAD", "SLEEPING" },
            { "Dead", "Sleeping" },
            { "dead", "sleeping" },

            { "HOME", "DEN" },
            { "Home", "Den" },
            { "home", "den" },
        };

        public override void Initialize()
        {
            SubscribeLocalEvent<OwOAccentComponent, AccentGetEvent>(OnAccent);
        }

        public string Accentuate(string message, OwOAccentComponent component)
        {
            message = message.Trim();

            // Replace words with other words
            foreach (var (word, repl) in SpecialWords)
                message = Regex.Replace(message, $@"(?<!\w){word}(?!\w)", repl);


            // If there should be emojis in the message
            if (component.Kaomoji)
            {
                // Random prefix
                if (_random.Prob(0.35f) && message.Length > 15)
                    message = $"{_random.Pick(Prefixes)} {message}";
            }
            else
            {
                // Random prefix
                if (_random.Prob(0.25f) && message.Length > 15)
                    message = $"{_random.Pick(nPrefixes)} {message}";
            }


            // If there should be emojis in the message
            if (component.Kaomoji)
            {
                // Exclaim
                if (message != "!")
                    message = message.Replace("! ", $"! {_random.Pick(Faces)} ");
                if (message.EndsWith("!") && message != "!")
                    message = message.Substring(0, message.Length - 1) + message.Substring(message.Length - 1).Replace("!", $"! {_random.Pick(Faces)}");

                // Question
                if (message != "?") message = message.Replace("? ", $"? {_random.Pick(CFaces)} ");
                if (message.EndsWith("?") && message != "?")
                    message = message.Substring(0, message.Length - 1) + message.Substring(message.Length - 1).Replace("?", $"? {_random.Pick(CFaces)}");
            }


            // If there should be emojis in the message
            if (component.Kaomoji)
            {
                // Random suffix, not affected by ! and ? reformatting and won't do if theres punctuation or some Kaomojis to end the message
                if (!message.EndsWith("!") && !message.EndsWith("?")
                    && !message.EndsWith(".") && !message.EndsWith(",")
                    && !message.EndsWith(")") && !message.EndsWith(";")
                    && message.Length > 15 && _random.Prob(0.3f))
                    message = $"{message}, {_random.Pick(Suffixes)}";
            }
            else
            {
                // Random suffix, not affected by ! and ? reformatting and won't do if theres punctuation or some Kaomojis to end the message
                if (!message.EndsWith("!") && !message.EndsWith("?")
                    && !message.EndsWith(".") && !message.EndsWith(",")
                    && !message.EndsWith(")") && !message.EndsWith(";")
                    && message.Length > 15 && _random.Prob(0.2f))
                    message = $"{message}, {_random.Pick(nSuffixes)}";
            }


            // Slur letters
            // Swuw wettews
            message = message
                .Replace("r", "w").Replace("R", "W")
                .Replace("l", "w").Replace("L", "W");


            return message;
        }

        private void OnAccent(EntityUid uid, OwOAccentComponent component, AccentGetEvent args)
        {
            args.Message = Accentuate(args.Message, component);
        }
    }
}
