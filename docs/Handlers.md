# Handlers

Handlers are composable bits of functionality that are _handling_ the input in a meaningful way.
Handlers have two responsibilities:

* Attempt to handle the input in a way meaningful to the handler and report the handling result (no
  match, success, and error)
* Print itself to generate a user-friendly help message.

Here's how the handler is described:

```c#
public enum HandleResult { NotMatched, Handled, HandledWithError }

public interface IHandler : IPrintable
{
    // handling is both checking whether the handler is capable of handling this input, and actually handling it
    // hence the need for the tri-state return type:
    // any input is either handled, successfully or not, or not handled at all
    Task<HandleResult> HandleAsync(string[] input);

    // this method is for printing itself for help message generation
    void PrintSelf(IPrinter printer);
}
```

Let's have a more detailed look with some examples.

---

## Token handler

The Token handler works by inspecting the next string argument and checking if it matches the
expected token. If it's a match, it delegates the rest of the string arguments (excluding the first
one!) to the wrapped subtree. If it's not a match, it'll return `HandleResult.NotMatched`.

### Code example:

```c#
using System;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class ForkExampleCli
    {
        public static Task<int> Main(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Token("hello",
                    Token("world",
                        Token("please ?",
                            Action((_, _) => Console.WriteLine("Hello!"))))))
        ).HandleAndGetExitCode(input);
    }
}
```

### Output:

Empty input, or input that doesn't completely match tokens in the specified order:

```
# cOOnsole.Playground // or anything, except the below example
cOOnsole.Playground v1.0.0
Usage:

  hello
    world
      please?
```

Correct input:

```
# cOOnsole.Playground hello world "please ?"
Hello!
```

---

## Fork handler

The Fork handler works by wrapping multiple child handlers and tries them in order until it finds a
handler that matches with this input and handles it, regardless of whether successfully or not.
Essentially, it's a `switch`-like construct.

### Code example:

```c#
using System;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class ForkExampleCli
    {
        public static Task<int> Main(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Fork(
                    Token("cmd1", Action((_, _) => Console.WriteLine("cmd1 called"))),
                    Token("cmd2", Action((_, _) => Console.WriteLine("cmd2 called"))),
                    Token("cmd3", Action((_, _) => Console.WriteLine("cmd3 called")))))
        ).HandleAndGetExitCode(input);
    }
}
```

### Output:

Without arguments:

```
# cOOnsole.Playground
cOOnsole.Playground v1.0.0
Usage:

  cmd1
  cmd2
  cmd3
```

With arguments:

```
# cOOnsole.Playground cmd1
cmd1 called
```

Etc.

---

## UntypedAction handler

The untyped action handler is a handler that performs specified `System.Action` on the remaining
unhandled command line arguments, and returns `HandleResult.Handled`. This is essentially a bridge
from `cOOnsole` code to the user code, if the user doesn't want to go full OO and wants to execute
some code in case the input matches some combination of tokens (see Token handler).

This handler is called untyped action because it doesn't do any command line argument parsing, and
user's `System.Action` is operating on `string[]` in this instance. There is another handler that
does the parsing, but I'll cover it later.

### Code example

```c#
using System;
using System.Data;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class UntypedActionExampleCli
    {
        private class Calculator
        {
            private readonly DataTable _dataTable = new();

            public double Calculate(string[] expressionTokens)
            {
                var result = _dataTable.Compute(string.Concat(expressionTokens), null);
                return result is DBNull ? 0.0 : Convert.ToDouble(result);
            }
        }

        public static Task<int> Main(string[] input)
        {
            var calculator = new Calculator();
            return Cli(
                PrintUsageIfNotMatched(
                    Token("calc",
                        Description("calculates arithmetic expressions",
                            Action((args, _) => Console.WriteLine(calculator.Calculate(args))))))
            ).HandleAndGetExitCode(input);
        }
    }
}
```

### Output

Argumentless call:

```
# cOOnsole.Playground
cOOnsole.Playground v1.0.0
Usage:

  calc
    calculates arithmetic expressions
```

Using calc command with arguments

```
# cOOnsole.Playground calc 2 + 2 * 2
6
```

---

## Description handler

This handler is not about actually handling the input, it's about generating a prettier help message
to the user. It's useful to group a list of commands within the `Fork` handler or to give
description to `Token`s.

### Code example:

```c#
using System;
using System.Threading.Tasks;
using cOOnsole.Handlers.Base;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class DescriptionExampleCli
    {
        public static Task<int> Main(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Description("this is my command group!",
                    Fork(
                        Token("command1", Description("this is command 1 description!",
                            Unconditional(HandleResult.NotMatched))),
                        Token("command2", Description("this is command 2 description!",
                            Unconditional(HandleResult.NotMatched))),
                        Token("command3", Description("this is command 3 description!",
                            Unconditional(HandleResult.NotMatched))))
                )
            )
        ).HandleAndGetExitCode(input);
    }
}
```

### Output:

```
# cOOnsole.Playground
cOOnsole.Playground v1.0.0
Usage:

  this is my command group!

    command1
      this is command 1 description!

    command2
      this is command 2 description!

    command3
      this is command 3 description!
```