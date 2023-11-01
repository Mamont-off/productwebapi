using ProductWebAPI.Models;

namespace ProductWebAPI.Other;

public class Node
{
    public Node? Root;
    public ProductModel Object;
    public List<Node> Children;
    
    public bool IsParent => Children.Count > 0;

    public Node(ProductModel newObject)
    {
        Children = new List<Node>();
        this.Object = newObject;
    }

    public void TryAdd(ProductModel newObject, int parentId)//return bool/object result?
    {
        if (Object.Id == parentId)
        {
            Children.Add(new Node(newObject){Root = this});
            return;
        }
        
        foreach (var child in Children)
        {
            child.TryAdd(newObject, parentId);
        }
    }
}