namespace Customer.DataStreaming.Source.Products;

// Represents a single IT book product
public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }
    public long Version { get; private set; }

    public Product(Guid id, string name, string description, decimal price, string category, long version = 1)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price <= 30 ? price : throw new ArgumentOutOfRangeException(nameof(price), "Price must be 30 or less.");
        Category = category;
        Version = version;
    }

    public void Update(Product product)
    {
        Name = product.Name;
        Description = product.Description;
        Price = product.Price <= 30 ? product.Price : throw new ArgumentOutOfRangeException(nameof(product.Price), "Price must be 30 or less.");
        Category = product.Category;
        Version++;
    }
}

// Static catalog containing a collection of IT book products
public static class ProductCatalog
{
    public static IReadOnlyList<Product> Products { get; } = new List<Product>
    {
        // a488c927-6062-4b0c-bf76-03dcbfe8c26e
        new Product(Guid.Parse("1e6803b2-5af0-43a4-9f7c-9923b6b1a571"), "Clean Code", "A Handbook of Agile Software Craftsmanship", 29.99m, "IT Books"),
        new Product(Guid.Parse("2e6803b2-5af0-43a4-9f7c-9923b6b1a572"), "The Pragmatic Programmer", "Your Journey to Mastery", 27.50m, "IT Books"),
        new Product(Guid.Parse("3e6803b2-5af0-43a4-9f7c-9923b6b1a573"), "Design Patterns", "Elements of Reusable Object-Oriented Software", 30.00m, "IT Books"),
        new Product(Guid.Parse("4e6803b2-5af0-43a4-9f7c-9923b6b1a574"), "Refactoring", "Improving the Design of Existing Code", 28.00m, "IT Books"),
        new Product(Guid.Parse("5e6803b2-5af0-43a4-9f7c-9923b6b1a575"), "Introduction to Algorithms", "Comprehensive algorithms guide", 25.99m, "IT Books"),
        new Product(Guid.Parse("6e6803b2-5af0-43a4-9f7c-9923b6b1a576"), "Code Complete", "A Practical Handbook of Software Construction", 29.95m, "IT Books"),
        new Product(Guid.Parse("7e6803b2-5af0-43a4-9f7c-9923b6b1a577"), "The Art of Computer Programming", "Comprehensive programming techniques", 30.00m, "IT Books"),
        new Product(Guid.Parse("8e6803b2-5af0-43a4-9f7c-9923b6b1a578"), "You Don't Know JS", "Deep dive into JavaScript", 22.99m, "IT Books"),
        new Product(Guid.Parse("9e6803b2-5af0-43a4-9f7c-9923b6b1a579"), "Domain-Driven Design", "Tackling Complexity in Software", 29.50m, "IT Books"),
        new Product(Guid.Parse("10e6803b-5af0-43a4-9f7c-9923b6b1a570"), "Java Concurrency in Practice", "Concurrency concepts in Java", 27.99m, "IT Books"),
        new Product(Guid.Parse("11e6803b-5af0-43a4-9f7c-9923b6b1a581"), "Head First Design Patterns", "A Brain-Friendly Guide", 30.00m, "IT Books"),
        new Product(Guid.Parse("12e6803b-5af0-43a4-9f7c-9923b6b1a582"), "Effective Java", "Best practices for Java programming", 29.99m, "IT Books"),
        new Product(Guid.Parse("13e6803b-5af0-43a4-9f7c-9923b6b1a583"), "Python Crash Course", "Hands-on project-based learning", 24.95m, "IT Books"),
        new Product(Guid.Parse("14e6803b-5af0-43a4-9f7c-9923b6b1a584"), "Groking Algorithms", "Illustrated guide to algorithms", 26.99m, "IT Books"),
        new Product(Guid.Parse("15e6803b-5af0-43a4-9f7c-9923b6b1a585"), "Kubernetes Up & Running", "Guide to Kubernetes", 28.99m, "IT Books"),
        new Product(Guid.Parse("16e6803b-5af0-43a4-9f7c-9923b6b1a586"), "Learn Python the Hard Way", "Best-selling Python guide", 20.00m, "IT Books"),
        new Product(Guid.Parse("17e6803b-5af0-43a4-9f7c-9923b6b1a587"), "Deep Learning with Python", "Comprehensive deep learning guide", 30.00m, "IT Books"),
        new Product(Guid.Parse("18e6803b-5af0-43a4-9f7c-9923b6b1a588"), "Artificial Intelligence: A Guide to Intelligent Systems", "Introduction to AI", 27.99m, "IT Books"),
        new Product(Guid.Parse("19e6803b-5af0-43a4-9f7c-9923b6b1a589"), "Spring in Action", "Comprehensive Spring framework guide", 29.50m, "IT Books"),
        new Product(Guid.Parse("20e6803b-5af0-43a4-9f7c-9923b6b1a590"), "Docker Deep Dive", "Comprehensive guide to Docker", 29.99m, "IT Books"),
        new Product(Guid.Parse("21e6803b-5af0-43a4-9f7c-9923b6b1a591"), "C# in Depth", "Mastering C# concepts", 28.50m, "IT Books"),
        new Product(Guid.Parse("22e6803b-5af0-43a4-9f7c-9923b6b1a592"), "The Clean Coder", "Professionalism in software development", 26.99m, "IT Books"),
        new Product(Guid.Parse("23e6803b-5af0-43a4-9f7c-9923b6b1a593"), "Programming Pearls", "Classic programming techniques", 27.50m, "IT Books"),
        new Product(Guid.Parse("24e6803b-5af0-43a4-9f7c-9923b6b1a594"), "Head First Java", "Brain-friendly Java guide", 29.95m, "IT Books"),
        new Product(Guid.Parse("25e6803b-5af0-43a4-9f7c-9923b6b1a595"), "Modern Operating Systems", "Comprehensive OS concepts", 30.00m, "IT Books"),
        new Product(Guid.Parse("26e6803b-5af0-43a4-9f7c-9923b6b1a596"), "Data Structures and Algorithms in Java", "Comprehensive guide to DS & A", 28.00m, "IT Books"),
        new Product(Guid.Parse("27e6803b-5af0-43a4-9f7c-9923b6b1a597"), "Fluent Python", "Guide to Python's advanced features", 29.99m, "IT Books"),
        new Product(Guid.Parse("28e6803b-5af0-43a4-9f7c-9923b6b1a598"), "Mastering Regular Expressions", "Comprehensive regex guide", 27.50m, "IT Books"),
        new Product(Guid.Parse("29e6803b-5af0-43a4-9f7c-9923b6b1a599"), "Compilers: Principles, Techniques, and Tools", "Comprehensive compiler design", 30.00m, "IT Books"),
        new Product(Guid.Parse("30e6803b-5af0-43a4-9f7c-9923b6b1a600"), "Designing Data-Intensive Applications", "Comprehensive guide to data systems", 29.99m, "IT Books"),

        // More books up to 60...
    };
}
