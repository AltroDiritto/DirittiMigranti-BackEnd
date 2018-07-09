using System;
using System.Collections.Generic;
using DirittoMigrantiAPI;
using DirittoMigrantiAPI.API;
using DirittoMigrantiAPI.Controllers;
using DirittoMigrantiAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DirittoMigrantiAPITest
{
    
     public class MessageExchangeControllerTest
    {/*
        //NOTA: Conversation in ambiente C# è già presente in Microsoft.CodeAnalysis.CSharp.Conversation, 
        //      quindi la classe è implementata sotto il nome di MessageExchange 

        //I consulenti
        User consultant1 = new Consultant("Francesca", "Loi", "francesca.loi@gmail.com");

        //Gli operatori
        User operator1 = new Operator("Mario", "Rossi", "mario.rossi@gmail.com", "MRARSS84E11H502L", "Operatore sociale", "Roma");
        User operator2 = new Operator("Aldo", "Neri", "aldo.neri@gmail.com", "LDANRE68M08E715Y", "Avvocato", "Palermo");

        MessageExchangesController messageExchangesController = new MessageExchangesController(new List<MessageExchange>());

        [Fact]
        public void TestModificaNote()
        {
            //Creazione di una conversazione da parte di un operatore
            Message message1 = new Message(operator1, "Salve, come devo comportarmi se X ha fatto Y?");
            MessageExchange messageExchange = messageExchangesController.NewConversation(message1);

            //Creazione di una nota
            string nota1 = "Nota1";
            string nota2 = "Nota2";

            messageExchangesController.EditNotesInConversation(messageExchange.Id, nota1);
            messageExchangesController.EditNotesInConversation(messageExchange.Id, nota2);

            //Verifico che la nota sia stata modificata correttamente
            Assert.Same(messageExchangesController.GetNotes(messageExchange.Id), nota2);
        }

        [Fact]
        public void TestGetConversations()
        {
            Message message1 = new Message(operator2, "Salve, cosa faccio se è successo X?");
            MessageExchange messageExchange1 = messageExchangesController.NewConversation(message1);

            Message message2 = new Message(operator1, "Salve, come devo comportarmi se X ha fatto Y?");
            MessageExchange messageExchange2 = messageExchangesController.NewConversation(message2);

            Message message3 = new Message(operator2, "Salve, come mi comporto nel caso Y?");
            MessageExchange messageExchange3 = messageExchangesController.NewConversation(message1);

            //Ottengo le conversazioni dell'operatore 2
            var conversations = messageExchangesController.GetConversationsByUser(operator2);

            //Verifico che ci siano solo le sue conversazioni
            Assert.Equal(conversations.Count, 2);
            Assert.True(conversations.Contains(messageExchange1));
            Assert.True(conversations.Contains(messageExchange3));
            Assert.False(conversations.Contains(messageExchange2));
        }
        */ }
  
}
