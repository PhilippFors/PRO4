using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct |
                       System.AttributeTargets.Interface,
                       AllowMultiple = true)
]
public class AuthorAttribute : System.Attribute
{
    public string mainAuthor;

    public string coAuthors;

    public AuthorAttribute(string _mainAuthor, string _coAuthors)
    {
        this.mainAuthor = _mainAuthor;
        this.coAuthors = _coAuthors;
    }
    public AuthorAttribute(string _mainAuthor)
    {
        this.mainAuthor = _mainAuthor;
    }
    public AuthorAttribute()
    {

    }
}
