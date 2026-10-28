# Exercise 5

Simulates a garage that stores a number of vehicles.

## Running

Run with `dotnet run`.

## Structure

`Garage` is the central collection of `Vehicle`s.
Different types of vehicles are in the `Vehicles/` directory.

`Ui/` contains helper classes for displaying a menu and reading input from the user.

`SearchFilter.cs` contains several classes that can filter search results.

## Feedback

I'm not too happy with `Menu`.
I think it should be possible to do a menu like this in a simpler way.
Honestly I'm not too thrilled about the UI in general, and wouldn't write a UI in this style if it weren't part of the exercise.