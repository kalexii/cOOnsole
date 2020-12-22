# cOOnsole

[![NuGet](https://img.shields.io/nuget/v/cOOnsole)](https://www.nuget.org/packages/cOOnsole/)
[![MyGet](https://img.shields.io/myget/kalexii/v/cOOnsole)](https://www.myget.org/feed/kalexii/package/nuget/cOOnsole/)
[![pipeline status](https://gitlab.kalexi.me/libraries/coonsole/badges/main/pipeline.svg)](https://gitlab.kalexi.me/libraries/coonsole)
[![coverage report](https://gitlab.kalexi.me/libraries/coonsole/badges/main/coverage.svg)](https://gitlab.kalexi.me/libraries/coonsole)

`cOOnsole` is an object-oriented console application framework. It draws heavy inspiration
from [React](https://reactjs.org/) and [Takes](https://github.com/yegor256/takes) framework.

Built with flexibility in mind, `cOOnsole` provides some amount of default must-have tools for
building CLI apps, and allows you to easily define your own.

## Getting Started

Let's start with a simple example.

### The code

```c#
using System;
using System.Linq;
using System.Threading.Tasks;
using static cOOnsole.Shortcuts;

namespace cOOnsole.Playground
{
    public static class SimpleExampleCli
    {
        public static Task<int> Main(string[] input) => Cli(
            PrintUsageIfNotMatched(
                Token("add",
                    Description("adds numbers",
                        Action((args, _) => Console.WriteLine(args.Select(double.Parse).Sum())))))
        ).HandleAndGetExitCode(input);
    }
}
```

### The output

#### Help message example

If the user provides no arguments, a help message is printed because no token matched (out of none,
since no tokens were provided), and the root handler is `PrintUsageIfNotMatched`. The same will
happen if user tries to input any tokens not handled by the application,
e.g. `cOOnsole.Playground hello world`.

```
# cOOnsole.Playground
cOOnsole.Playground v1.0.0
Usage:

  add
    adds numbers
```

#### Token matching example

If the user provides arguments that are handled by the application (in our example, `add` argument
is matched with `Token("add")`), the control is going to be directed into it's inner `Action()`
handler, which adds the remaining arguments.

```
# cOOnsole.Playground add 1 2 3 4 5
15
```

---

Documentation:
- [What are handlers?](./docs/Handlers.md)
- [More complex example](./docs/ComplexExample.md)