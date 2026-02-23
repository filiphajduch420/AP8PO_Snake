# Snake – Refaktoring podle Clean Code

## 1. Přejmenování proměnných

Původní názvy proměnných byly špatně čitelné, některé byly dokonce v holandštině, takže vůbec nebylo jasné co znamenají. Přejmenovali jsme je tak, aby dávaly smysl:

- `screenwidth` -> `windowWidth`
- `screenheight` -> `windowHeight`
- `randomnummer` -> `random`
- `gameover` -> `isGameOver` (změněno taky z int na bool)
- `hoofd` -> `snakeHead`
- `hoofd.xpos` -> `snakeHead.X`
- `hoofd.ypos` -> `snakeHead.Y`
- `hoofd.schermkleur` -> `snakeHead.Color`
- `movement` -> `currentDirection`
- `xposlijf` -> `bodyXPositions`
- `yposlijf` -> `bodyYPositions`
- `berryx` -> `foodX`
- `berryy` -> `foodY`
- `tijd` -> `frameStartTime`
- `tijd2` -> `currentTime`
- `buttonpressed` -> `directionChangedThisTick` (změněno ze stringu na bool)
- `pixel` (třída) -> `SnakeHead`

Kromě přejmenování jsme taky změnili typ u `gameover` z int (0/1) na bool, protože to je buď true nebo false. A `buttonpressed` byl původně string "no"/"yes", což taky nedávalo smysl, tak je z toho teď bool.

## 2. Rozdělení kódu na metody

Celý kód byl původně v jedné obrovské metodě `Main()`. Podle Clean Code (kapitola 3) by měla každá metoda dělat jen jednu věc. Takže jsme to rozsekali na menší metody:

- `Main()` – volá jen init, herní smyčku a game over
- `InitializeGame()` – nastaví okno, hada a jídlo
- `RunGameLoop()` – hlavní herní smyčka
- `CheckBorderCollision()` – kontrola nárazu do stěny
- `DrawBorders()` – vykreslení okrajů
- `DrawHorizontalBorder()` – vykreslí vodorovný okraj
- `DrawVerticalBorder()` – vykreslí svislý okraj
- `CheckFoodCollision()` – kontrola jestli had snědl jídlo
- `DrawBodyAndCheckSelfCollision()` – vykreslí tělo a kontroluje náraz do sebe
- `DrawSnakeHead()` – vykreslí hlavu
- `DrawFood()` – vykreslí jídlo
- `HandleInputForFrame()` – čte vstup z klávesnice
- `TryChangeDirection()` – změní směr podle stisknuté klávesy
- `UpdateSnakePosition()` – posune hada
- `MoveHead()` – posune hlavu jedním směrem
- `RemoveTailIfTooLong()` – ořízne ocas když je moc dlouhý
- `DisplayGameOver()` – zobrazí skóre na konci

Díky tomu je teď Main() úplně jednoduchý a celý program se dá snadno číst.
