# Secure Munchester United

Welcome to Secure Munchester United on Exercism's C# Track.
If you need help running the tests or submitting your code, check out `HELP.md`.
If you get stuck on the exercise, check out `HINTS.md`, but try and solve it without using those first :)

## Introduction

Casting and type conversion are different ways of changing an expression from one data type to another.

An expression can be cast to another type with the cast operator `(<type>)`.

```csharp
long l = 1000L;
int i = (int)l;

object o = new Random();
Random r = (Random)o;
```

If the types are not compatible an instance of `InvalidCastException` is thrown. In the case of numbers this indicates that the receiving type cannot represent the cast value. In the case of classes, one of the types must be derived from the other (this also applies trivially to structs).

An alternative to _casting_ is _type conversion_ using the `is` operator. This is typically applied to reference and nullable types.

```csharp
object o = new Random();
if (o is Random rand)
{
    int ii = rand.Next();
    // now, do something random
}
```

If you need to detect the precise type of an object then `is` may be a little too permissive as it will return true for a class and any of the classes and interfaces from which it is derived directly or indirectly. `typeof` and `Object.GetType()` are the solution in this case.

```csharp
object o = new List<int>();

o is ICollection<int> // true
o.GetType() == typeof(ICollection<int>) // false
o is List<int> // true
o.GetType() == typeof(List<int>) // true
```

## Instructions

Our football club [exercise:csharp/football-match-reports]() is soaring in the leagues, and you have been invited to do some more work, this time on the security pass printing system.

The class hierarchy of the backroom staff is as follows

```
TeamSupport (interface)
├ Chairman
├ Manager
└ Staff (abstract)
    ├ Physio
    ├ OffensiveCoach
    ├ GoalKeepingCoach
    └ Security
        ├ SecurityJunior
        ├ SecurityIntern
        └ PoliceLiaison
```

A complete implementation of the hierarchy is provided as part of the source code for the exercise.

All data passed to the security pass maker has been validated and is guaranteed to be non-null.

## 1. Get display name for a member of the support team as long as they are staff members

Please implement the `SecurityPassMaker.GetDisplayName()` method. It should return the value of the `Title` field instances of all classes derived from `Staff` and, otherwise, "Too Important for a Security Pass".

```csharp
var spm = new SecurityPassMaker();
spm.GetDisplayName(new Manager());
// => "Too Important for a Security Pass"
spm.GetDisplayName(new Physio());
// => "The Physio"
```

## 2. Customize the display name for the security team

Please modify the `SecurityPassMaker.GetDisplayName()` method. It should behave as in Task 1 except that if the staff member is a member of the security team (either of type `Security` or one of its derivatives) then the text " Priority Personnel" should be displayed after the title.

```csharp
var spm = new SecurityPassMaker();
spm.GetDisplayName(new Physio());
// => "The Physio"
var spm2 = new SecurityPassMaker();
spm2.GetDisplayName(new Security());
// => "Security Team Member Priority Personnel"
spm2.GetDisplayName(new SecurityJunior());
// => "Security Junior Priority Personnel"
```

## 3. Only designate principal security team members as priority personnel

Please modify the `SecurityPassMaker.GetDisplayName()` method. It should behave as in Task 2 except that the text " Priority Personnel" should not be displayed for instances of type `SecurityJunior`, `SecurityIntern` and `PoliceLiaison`.

```csharp
var spm2 = new SecurityPassMaker();
spm2.GetDisplayName(new Security());
// => "Security Team Member Priority Personnel"
spm2.GetDisplayName(new SecurityJunior());
// => "Security Junior"
```

## Source

### Created by

- @mikedamay

### Contributed to by

- @ErikSchierboom
- @yzAlvin