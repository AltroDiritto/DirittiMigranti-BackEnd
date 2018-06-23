using System;
using DirittoMigrantiAPI.Models;

using Xunit;

namespace DirittoMigrantiAPITest
{
    public class MessageExchangeTest
    {
        //NOTA: Conversation in ambiente C# è già presente in Microsoft.CodeAnalysis.CSharp.Conversation, 
        //      quindi la classe è implementata sotto il nome di MessageExchange 

        //I consulenti
        User consultant1 = new Consultant("Francesca", "Loi", "francesca.loi@gmail.com");
        User consultant2 = new Consultant("Piero", "Bruni", "piero.bruni@gmail.com");

        //Gli operatori
        User operator1 = new Operator("Mario", "Rossi", "mario.rossi@gmail.com", "MRARSS84E11H502L", "Operatore sociale", "Roma");
        User operator2 = new Operator("Aldo", "Neri", "aldo.neri@gmail.com", "LDANRE68M08E715Y", "Avvocato", "Palermo");

        [Fact]
        public void TestNuovaConversazione()
        {
            //Creazione di una conversazione da parte di un consulente
            Message message1 = new Message(consultant1, "Messaggio di test");
            Assert.Throws<ArgumentException>(() =>
            {
                MessageExchange conversazione1 = new MessageExchange(message1);
            });

            //Creazione di una conversazione da parte di un operatore
            Message message2 = new Message(operator1, "Salve, come devo comportarmi se X ha fatto Y?");
            MessageExchange conversazione2 = new MessageExchange(message2);
        }

        [Fact]
        public void TestDirittiConversazione()
        {
            //Creazione di una conversazione da parte di un operatore
            Message message1 = new Message(operator1, "Salve, come devo comportarmi se X ha fatto Y?");
            MessageExchange conversazione1 = new MessageExchange(message1);

            //Controllo dei diritto di accesso alla conversazione
            Assert.True(conversazione1.IsThisUserInTheConversation(operator1));
            Assert.False(conversazione1.IsThisUserInTheConversation(operator2));
        }

        [Fact]
        public void TestAggiuntaMessaggio()
        {
            //Creazione di una conversazione da parte di un operatore
            Message message1 = new Message(operator1, "Salve, come devo comportarmi se X ha fatto Y?");
            MessageExchange conversazione1 = new MessageExchange(message1);

            //Risposta da parte di un consulente
            Message message2 = new Message(consultant1, "Salve, deve fare Z e poi W");
            bool esito1 = conversazione1.AddMessage(message2);

            //Risposta da parte di un operator che non ha i diritti
            Message message3 = new Message(operator2, "Grazie");
            bool esito2 = conversazione1.AddMessage(message3);

            //Risposta da parte di un operator che ha i diritti
            Message message4 = new Message(operator1, "Grazie");
            bool esito3 = conversazione1.AddMessage(message4);

            //Il messaggio è stato aggiunto correttamente solo se si avevano i diritti per poterlo fare
            Assert.True(esito1);
            Assert.False(esito2);
            Assert.True(esito3);
        }
    }
}
