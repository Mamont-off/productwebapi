using ProductWebAPI.Models;

namespace ProductWebAPI.Other;

public sealed class SumPrice
{
    public readonly Node Root;
    
    public SumPrice(ProductModel rootObject)
    {
        Root = new Node(rootObject);
    }

    public void Add(ProductModel newObject, int parentId)
    {
        if (Root.Object.Id == parentId)
        {
            Root.Children.Add(new Node(newObject){Root = this.Root});
        }

        foreach (var child in Root.Children)
        {
            child.TryAdd(newObject, parentId);
        }
    }

    public List<ProductModel> CalculateProductSum()
    {
        var productWithSum = new List<ProductModel>();

        if (Root.IsParent)
        {
            var lastChild = Root.Children[0];
            while (Root.Object.Sum == 0)//FIXME: can be buggy
            {
                if (lastChild.IsParent)
                {
                    lastChild = lastChild.Children[0];
                }
                else
                {
                    var childWithChildId = lastChild.Root.Children.FindIndex(c => c.IsParent);
                    if (childWithChildId < 0)
                    {
                        decimal allChildrenPrices = 0;
                        foreach (var rootChild in lastChild.Root.Children)
                        {
                            if (rootChild.Object.Sum == 0)
                            {
                                rootChild.Object.Sum = rootChild.Object.Price * rootChild.Object.Count;
                            }

                            allChildrenPrices += rootChild.Object.Sum;
                            
                            productWithSum.Add(rootChild.Object);
                        }

                        lastChild.Root.Object.Sum = 
                            (allChildrenPrices + lastChild.Root.Object.Price) * lastChild.Root.Object.Count;
                        lastChild.Root.Children.Clear();
                        
                        lastChild = lastChild.Root;
                    }
                    else
                    {
                        lastChild = lastChild.Root.Children[childWithChildId];
                    }
                }
            }
            
            productWithSum.Add(Root.Object);
        }
        else
        {
            Root.Object.Sum = Root.Object.Price*Root.Object.Count;
            productWithSum.Add(Root.Object);
        }

        productWithSum.Reverse();//wrong order
        return productWithSum;
    }
}