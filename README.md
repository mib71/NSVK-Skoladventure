# Skoladventure

A port of the classic 1985 text adventure originally published in 
*Spectraview*, the magazine of Nordiska SpectraVideo Klubben (NSVK).

The original game was designed and programmed by Jon Wätte and Mikael 
Gajecki, and published in Spectraview År 2, Nr 1-3 (1985).

## The Story

You have been locked inside the school by the mad chemistry teacher. 
He is conducting dangerous experiments and the school could explode at 
any moment. You have 40 moves to escape before it is too late.

## Commands

- `go n/s/e/w` — Move in a direction
- `take [item]` — Pick up an item
- `drop [item]` — Drop an item
- `look` — Look around the room
- `search` — Search the room for hidden items
- `examine [item]` — Examine an item in your inventory
- `inventory` — List what you are carrying
- `drill [door/lock]` — Drill through a lock
- `connect [drill]` — Connect the drill to a power outlet
- `unlock [door/lock]` — Unlock a door with a key
- `drink [item]` — Drink something
- `use [item]` — Use an item
- `read [item]` — Read an item
- `q` — Quit

## Built With

- .NET 10
- C# 12

## Credits

Original game: Jon Wätte & Mikael Gajecki, NSVK 1985  
Original magazines available at: https://www.samdal.com/svdocuments.htm  
Port to C#: 2026 MiB

### Other ports
[Jonathan David Gilbert](https://github.com/logiclrd/QBX/blob/main/Samples/SKOLADV.BAS) — 
Faithful transcription to QuickBASIC with English localization, preserving the original 
line numbers and single-letter variable names.
