using System;
using System.Collections.Generic;

public class Chat {
    private LinkedList<Message> Messages = new LinkedList<Message>();

    public Chat(){
        Messages.AddFirst(new Message("Adam ty"));
        Messages.AddLast(new Message("chuju"));
        Messages.AddLast(new Message("Dziala"));
    }

    public void AddMessage(Message message){
        Messages.AddLast(message);
    }

    public void Print(){
        foreach (var item in Messages){
            Console.WriteLine(item.Data);
        }
    }


}