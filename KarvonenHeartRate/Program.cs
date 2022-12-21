using Spectre.Console;

AnsiConsole.Markup("[green]Karvonen Heart Rate[/] ");
Console.WriteLine();
Console.WriteLine();

var age = AnsiConsole.Prompt(
  new TextPrompt<int>("How old are you?")
    .PromptStyle("green")
    .ValidationErrorMessage("[red]That's not a valid age[/]")
    .Validate(age =>
        age switch
        {
            <= 16 => ValidationResult.Error("[red]You must at least be 16 years old.[/]"),
            >= 101 => ValidationResult.Error("[red]You must be younger than 100.[/]"),
            _ => ValidationResult.Success(),
        })
    );

var restingHeartRate = AnsiConsole.Prompt(
  new TextPrompt<int>("What is your resting heart rate?")
      .PromptStyle("green")
      .ValidationErrorMessage("[red]That's not a valid heart rate[/]")
      .Validate(rhr =>
          rhr switch
          {
              <= 20 => ValidationResult.Error("[red]Your resting heart rate should be greater than 20.[/]"),
              >= 101 => ValidationResult.Error("[red]Your resting heart rate should NOT be greater than 100.[/]"),
              _ => ValidationResult.Success(),
          })
      );

decimal lowerBounds = 55m;
decimal upperBounds = 95m;
int step = 5;

var table = new Table();
table.AddColumn("Intensity");
table.AddColumn("Rate");

for (decimal counter = lowerBounds; counter <= upperBounds; counter += step)
{
    var intensity = counter / 100;

    var target = (int)(((220 - age - restingHeartRate) * intensity) + restingHeartRate);
    table.AddRow($"{intensity:P0}", $"{target} bpm");
}

Console.WriteLine();
Console.WriteLine($"Your maximum heart rate: {220 - age}");
Console.WriteLine();

AnsiConsole.Write(table);
