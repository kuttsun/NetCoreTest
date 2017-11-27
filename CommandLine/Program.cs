using System;
using Microsoft.Extensions.CommandLineUtils;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                // アプリケーション名（ヘルプの出力で使用される）
                Name = "CommandLineTest",
            };

            // ヘルプ出力のトリガーとなるオプションを指定
            app.HelpOption("-?|-h|--help");

            // オプションの設定
            var commonOptions = app.Option("--common|-common",
                "common オプション",
                CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                Console.WriteLine("Hello World!");
                if (commonOptions.HasValue()) Console.WriteLine("共通オプションあり");
                return 0;
            });

            app.Command("hoge", (command) =>
            {
                // 説明（ヘルプの出力で使用される）
                command.Description = "Hogeを出力する";

                // コマンドについてのヘルプ出力のトリガーとなるオプションを指定
                command.HelpOption("-?|-h|--help");

                // コマンドの引数（名前と説明を引数で渡しているが、これはヘルプ出力で使用される）
                var hogeArgs = command.Argument("[Hogeの引数]", "Hogeの引数の説明");

                // オプションの設定
                var hogeOptions = command.Option("-o|--option <opitons>",
                    "hogeのオプション",
                    CommandOptionType.MultipleValue);

                command.OnExecute(() =>
                {
                    var location = hogeArgs.Value != null ? $"引数あり {hogeArgs.Value}" : "引数なし";
                    Console.WriteLine("Hoge: " + location);

                    foreach (var value in hogeOptions.Values)
                    {
                        Console.WriteLine("Hogeのオプション: " + value);
                    }

                    // Command 内でオプションの定義をしていないので、これは必ず false になる
                    // (Command の外で定義したオプションは Command 内では有効にならない)
                    if (commonOptions.HasValue()) Console.WriteLine("common オプションあり");
                    return 0;
                });
            },
            // 未定義のオプションがあった場合に例外とするかどうか
            false);

            app.Execute(args);

            Console.ReadKey();
        }
    }
}
