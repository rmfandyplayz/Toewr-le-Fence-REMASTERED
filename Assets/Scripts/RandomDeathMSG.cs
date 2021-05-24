using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDeathMSG : MonoBehaviour
{
    public List<string> death_messages = new List<string> {
        "bruh moment",
        "smh get gud noob",
        "you didnt know the way",
        "RIP",
        "You're so bald that even a wig won't help",
        "You're such a loser that if you participated in a competition for the best loser, you would get 2nd",
        "People have beaten harder games with a steering wheel. And here you are being bad at this game.",
        "Go back to watching anime, this game isn't for you",
        "Nice try, but not really",
        "F",
        "Playing Fortnite doesn't make you better",
        "Go sub to T series instead",
        "Hey if you keep this up Putin will exile you to Siberia",
        "Adopt me in roblox might be a good game for you",
        "Dude stop looking at those anime girls while playing",
        "BALD",
        "Lol Rip",
        "I diagnose you with idioticsm",
        "I diagnose you with stupidism",
        "I diagnose you with cringe",
        "No wonder why your dad never came back when he went to get milk",
        "Wow you're so bad",
        "BAD! BAD! U FLIPPIN SUCK",
        "You suck",
        "Do you are have stupid?",
        "You is a stupid",
        "Sucks to be you",
        "Exactly why your mom called you a dissappointment",
        "Imagine getting destroyed by some colored spheres",
        "Smh think ur better off doing homework",
        "You've been diagnosed bad at the game by 69+ doctors",
        "Even FLEX TAPE can't fix how bad you are!",
        "Looks like god created the middle finger because of you",
        "You're so disappointing, the happy meal cried!",
        "You're making an onion cry because of how bad you are",
        "Your generation's gene pool needs a life guard. Otherwise the rest of your generation will be a nightmare",
        "Even your GRANDMA is much better at the game than you are",
        "I see puny human attempting big task, I also see puny human failing, puny human funny",
        "I don't think you can get a girlfriend with that IQ",
        "Your stupidity... It generates gravity!",
        "You're so bad, even the Devil himself doesn't want you in h3ll!",
        "Yea you tried. That's what's depressing",
        "Did you even try?",
        "Your grade in this game is an F, and your grade in school is also an F because you weren't doing your homework.",
        "Do you even know how to play?",
        "Go cry in your crappy corner while you get destroyed by a bot",
        "even baldi is shamed at your bald head",
        "your brain makes a grain of sand look like the moon",
        "your iq is smaller than your ego",
        "imagine wasting your money on a game that you get demolished by",
        "This game has no mercy, which is somthing you dont have",
        "this game is awesome, you're the problem here",
        "here you deserve a seccond chance... wait that was your seccond chance. such a failure",
        "How can failures like you play this game. Such a disgrace",
        "u thought you were a god at this game, well this game is god.",
        "this game is not trying your just bad",
        "this game enjoys crushing your dreams",
        "u suck lol retard",
        "I heard that there is a stupid player playing this game. Its most likely you",
        "your not worthy to win. Wait. you cant win. lol loser",
        "maybe dragon city is a good game for you",
        "this game has no feelings, but enjoys feeling your pain",
        "Pain is experience, experience is utter failure. you are experience",
        "you're so bad even Satan cringes",
        "imagine trying on a game that wastes your time",
        "Your so bad even your mom cringes",
        "you so bad at this game, even joker frowns",
        "when you play this game, even superheros cant save you",
        "Imagine being as good as you... I would die",
        "How did you even spend money on this game if you are such a failure in life",
        "You should go back and cry to your mom... Oh wait she left you",
        "ew you died",
        "You have a smooth brain",
        "Should've spent that $1 on something you're actually good at",
        "Your new brain is ready.",
        "O: OWO you ded",
        "I thought your IQ was at least 1 :<",
        "Your IQ makes the smallest Neurons look like the Universe",
        "*facepalm*",
        "I give you a B, for Better luck next time",
        "Wait for it… Wait for it… Wait for it… AAANNNDDD ya ded",
        "At least grow a neuron in you skull",
        "Go back to Kindergarten",
        "Welcome to the Afterlife, maybe you wouldn't be here if you had a brain",
        "You made a mistake, just like your parents did when making you",
        "Your iq is lower than the temperature in your room... In Celsius",
        "Imagine dying xDDDDDDDDDDD",
        "What is wrong with you????",
        "You're better off playing Fortnite lol noob",
        "Go play Minecraft it's gonna be better for ur health",
        "Bro this game aint hard u just have a small brain... Wait no you don't even have one",
        "When god rolled for your stats, the IQ portion of the stat rolled a 1",
        "You lost... I recommend subscribing to RMF_AndyPlayz while you suffer from failure",
        "Mistakes are a part of learning... But your parents didn't learn from their previous mistake and created you. The previous mistake was your older brother",
        "Yea you seem like a noob Fortnite player playing this game..."
    };


    void Start()
    {
        GetComponent<Text>().text = RandomMessage();

    }

    private string RandomMessage()
    {
        int randomnumber = Random.Range(0, death_messages.Count);
        return death_messages[randomnumber];
        
    }


}
