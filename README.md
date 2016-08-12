# OpenQuery
OpenQuery - query building helper for SQL-like databases
Easy to use in LINQ-like syntax.

Example:

```
var defaultSelect = Query.With<SqLiteImplementation>()
                .Select("Id", "Name")
                .From<Model>()
                .Where()
                .AreEqual<Model, int>(x => x.Id, 1)
                .Or()
                .AreEqual<Model, int>(x => x.Id, 2)
                .Build();
```                
Result: 
> "SELECT Id, Name FROM Model WHERE Id = 1 OR Id = 2"
