namespace ConsoleAppFramework.Context
{
    public class CliContext<TGlobalArguments>
    {
        private readonly Cli cli;

        public CliContext(Cli cli) => this.cli = cli;

        public string[] CurrentTokenPath { get; internal set; }

        public string[] AllArguments { get; internal set; }

        public string[] RemainingArguments { get; internal set; }

        public TGlobalArguments GlobalArgument { get; set; }

        public CliContext<TGlobalArguments, TLocalArguments> Bind<TLocalArguments>(TLocalArguments arguments)
            => new CliContext<TGlobalArguments, TLocalArguments>(cli)
            {
                AllArguments = AllArguments,
                GlobalArgument = GlobalArgument,
                RemainingArguments = RemainingArguments,
                CurrentTokenPath = CurrentTokenPath,
                Argument = arguments
            };

        public void PrintHelp() => cli.PrintSelf();
    }

    public class CliContext<TGlobalArgument, TLocalArgument> : CliContext<TGlobalArgument>
    {
        public TLocalArgument Argument { get; set; }

        public CliContext(Cli cli) : base(cli)
        {
        }
    }
}