using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment
{
    private int level;
    private string content;
    private string answer;
    private int id;

    public Assignment(int id, int level, string content, string answer)
    {
        this.Level = level;
        this.Content = content;
        this.Answer = answer;
        this.id = id;
    }

    public int Level { get => level; set => level = value; }
    public string Content { get => content; set => content = value; }
    public string Answer { get => answer; set => answer = value; }
}
