//Нехай нам треба створити деяку багаторівневу файлову систему,
//тобто в нас є головна папка, яка може зберігати у собіяк інші
//папки, так і різні файли, інші папки також можутьь зберігати
//файли та папки і тд.
//Задача: організувати таку систему

class Program
{
    static void Main(string[] args)
    {
        Component fileSystem = new Folder("Файлова система");
        Component Folder_1 = new Folder("Папка 1");
        Component pngFile_1 = new File("image1.png");
        Component docxFile = new File("document.docx");
        // добавляємо файли у папку 1
        Folder_1.Add(pngFile_1);
        Folder_1.Add(docxFile);
        // добавляємо папку з файлами у файлову систему
        fileSystem.Add(Folder_1);
        // демонструємо структуру
        fileSystem.Print(0);
        Console.WriteLine();
        // видаляємо файлик малюнок з папки
        Folder_1.Remove(pngFile_1);
        // створюємо нову папку
        Component Folder_2 = new Folder("Папка 2");
        // добавляем в нее файлы
        Component txtFile = new File("something.txt");
        Component pngFile_2 = new File("image2.png");
        Folder_2.Add(txtFile);
        Folder_2.Add(pngFile_2);
        Folder_1.Add(Folder_2);

        fileSystem.Print(0);

        Console.Read();
    }
}

abstract class Component // абстрактний клас, який описує компонент (чи то папка чи то файл)
{
    protected string name;

    public Component(string name)
    {
        this.name = name;
    }

    public virtual void Add(Component component) { }

    public virtual void Remove(Component component) { }

    public virtual void Print(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);
    }
}
class Folder : Component // клас, який описує папки
{
    private List<Component> components = new List<Component>();

    public Folder(string name): base(name)
    {
    }

    public override void Add(Component component)
    {
        components.Add(component);
    }

    public override void Remove(Component component)
    {
        components.Remove(component);
    }

    public override void Print(int depth)
    {
        Console.WriteLine(new String('-', depth) + name);
        for (int i = 0; i < components.Count; i++)
        {
            components[i].Print(depth + 2);
        }
    }
}

class File : Component // клас, який описує файли
{
    public File(string name): base(name)
    { }
}