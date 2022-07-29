SMOOTH GRID LAYOUT

Works in playmode only!!!

Package provides a "workaround" for unity regular grid layout alignment
and makes it possible to smooth out UI elements movement in grid layout
group.

Usage: Inside hierarchy window right-click -> UI -> Smooth Grid Layout.
Then put your UI elements under "Elements Container" gameobject. They
will be automatically sized according to the grid cell size.

The way it works: for every ui element that you put under "Elements
Container" it creates invisible placeholders and puts those under
"Placeholders container". Then it links added ui elements to corresponding
placeholders so that they lerp to placeholders position.