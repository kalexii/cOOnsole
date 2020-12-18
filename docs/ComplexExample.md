# More complex example

---

In this example, we are going to touch on help messages and argument parsing.

---

## The code

```c#
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using cOOnsole.Playground.SimpleExample;
using static cOOnsole.Shortcuts; // to use Cli, Fork, Token, etc. without new ...()

internal static class Program
{
    public static Task<int> Main(string[] input) => Cli(
        PrintUsageIfNotMatched(
            Description("commands", Fork(
                Token("echo", Description("this command simply prints the arguments given to it",
                    Action((rest, _) =>
                        Console.WriteLine("echo! rest: {0}", string.Join(", ", rest))))),
                Token("parse", Description("this command parses the arguments and prints them",
                    Action<SimpleExampleParams>((arg, _) =>
                        Console.WriteLine(ToJson(arg))))),
                Token("help", Description("this command prints this help message",
                    Unconditional(HandleResult.NotMatched))))))
    ).HandleAndGetExitCode(input);

    private enum Option { First, Second, Third }

    private class SimpleExampleParams
    {
        [Argument("--bool", "-b", Description = "Required bool parameter")]
        public bool BoolParam { get; set; }

        [Argument("--str", "-s", Description = "Required string parameter")]
        public string StringParam { get; set; } = default!;

        [Argument("--int", "-i", Description = "Required int parameter")]
        public int IntParam { get; set; }

        [Argument("--enum", "-e", Description = "Required enum parameter")]
        public Option EnumParam { get; set; }

        [Argument("--intArray", "-ia", Description = "Required int array parameter")]
        public int[] IntArrayParam { get; set; } = default!;

        [Argument("--enum-list", "-el", Description = "Required enum list parameter")]
        public List<Option> EnumListParam { get; set; } = default!;
    }
}
```

---

## The output

- Help message. This will be printed if the `help` command is used, or if none of the commands
  match.

```
# cOOnsole.Playground
cOOnsole.Playground v1.0.0
Usage:

  commands

    echo
      this command simply prints the arguments given to it

    parse
      this command parses the arguments and prints them

        required arguments:
                --bool | -b : Boolean                        # Required bool parameter
                 --str | -s : String                         # Required string parameter
                 --int | -i : Int32                          # Required int parameter
                --enum | -e : (First | Second | Third)       # Required enum parameter
           --intArray | -ia : Int32[]                        # Required int array parameter
          --enum-list | -el : List<(First | Second | Third)> # Required enum list parameter

    help
      this command prints this help message
```

- `echo` command, that prints all of the arguments that come after it

```
# cOOnsole.Playground echo 123 hello world 456
echo! rest: 123, hello, world, 456
```

- `parse` command, that parses the list of command line arguments into an object of
  type `SimpleExampleParams`, and simply prints it in JSON.

```
# cOOnsole.Playground parse --bool --str "my string" --int -127 --enum first --intArray 1 2 3 --enum-list First Second 
{
  "BoolParam": true,
  "StringParam": "my string",
  "IntParam": -127,
  "EnumParam": "First",
  "IntArrayParam": [
    1,
    2,
    3
  ],
  "EnumListParam": [
    "First",
    "Second"
  ]
}
```