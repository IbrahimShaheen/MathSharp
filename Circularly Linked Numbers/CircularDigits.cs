using System;

class CircularDigits
{
    private IntNode First;
    public int Length;

    public CircularDigits(int Num)
    {
        int Length = 0;
        IntNode Prev = null;
        IntNode Last = null;
        while (Num != 0)
        {
            IntNode Curr = new IntNode(Num % 10, Prev);
            if (Length == 0) Last = Curr;
            Num /= 10;
            Length++;
            Prev = Curr;
        }
        Last.Next = Prev;

        this.First = Prev;
        this.Length = Length;
    }

    // Returns the number stored int the Digits from StartingIndex
    public int GetNum(int StartingIndex)
    {
        IntNode StartNode = First;
        for (int i = 0; i < StartingIndex; i++) StartNode = StartNode.Next;

        string Num = "";
        IntNode Curr = StartNode;
        for (int i = 0; i < Length; i++)
        {
            Num = Num + Curr.Value;
            Curr = Curr.Next;
        }

        return Int32.Parse(Num);
    }
}