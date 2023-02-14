using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClass : MonoBehaviour
{
    
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        public enum Name
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public Suit theSuite;
        public Name theName;
        public int value;

        public CardClass(Suit suit, Name name)
        {
            this.theSuite = suit;
            this.theName = name;

            switch (name)
            {
                case Name.Ace:
                    this.value = 11;
                    break;
                case Name.Two:
                    this.value = 2;
                    break;
                case Name.Three:
                    this.value = 3;
                    break;
                case Name.Four:
                    this.value = 4;
                    break;
                case Name.Five:
                    this.value = 5;
                    break;
                case Name.Six:
                    this.value = 6;
                    break;
                case Name.Seven:
                    this.value = 7;
                    break;
                case Name.Eight:
                    this.value = 8;
                    break;
                case Name.Nine:
                    this.value = 9;
                    break;
                default:
                    this.value = 10;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{theName} of {theSuite}";
        }

        public static List<CardClass> AllCards
        {
            get
            {
                List<CardClass> cards = new List<CardClass>();
                foreach (Suit suit in System.Enum.GetValues(typeof(Suit)))
                {
                    foreach (Name name in System.Enum.GetValues(typeof(Name)))
                    {
                        cards.Add(new CardClass(suit, name));
                    }
                }
                return cards;
            }
        }
    

}
