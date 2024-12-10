﻿![Screenshot](https://github.com/TheRinzler65/TwilightBoxart/blob/main/img/screenshot.png)

# Twilight Boxart

**PROJET ORIGINAL EN MODE MAINTENANCE**

Plus d'informations ci-dessous :

> Ce projet a été forké dans le but de passer d'une ancienne version obsolète de .NET Core (incompatible avec Linux en raison d'une prise en charge dépassée d'OpenSSL) à une version actuelle de .NET (compatible avec Windows, MacOS et Linux).
>
> Aucun ajout de fonctionnalités n'est prévu, à part assurer son bon fonctionnement et nettoyer du code inutilisé.
>
> Les systèmes d'exploitation pris en charge sont ceux supportés par .NET 8.0.

Un téléchargeur de boxart écrit en C#. Utilise différentes sources et méthodes de scan pour déterminer les boxart appropriées.
Écrit pour [TwilightMenu++](https://github.com/DS-Homebrew/TWiLightMenu) mais peut être utilisé pour d'autres interfaces de chargement avec quelques ajustements de configuration. 😊

## Types de ROM pris en charge
Système | Correspondance (par ordre)
 --- | ---
 Nintendo - Game Boy | (sha1 / filename)
 Nintendo - Game Boy Color | (sha1 / filename)
 Nintendo - Game Boy Advance | (sha1 / filename)
 Nintendo - Nintendo DS | (titleid / sha1 / filename)
 Nintendo - Nintendo DSi | (titleid / sha1 / filename)
 Nintendo - Nintendo DSi (DSiWare) | (titleid / sha1 / filename)
 Nintendo - Nintendo Entertainment System | (sha1 / filename)
 Nintendo - Super Nintendo Entertainment System | (sha1 / filename)
 Nintendo - Family Computer Disk System | (sha1 / filename)
 Sega - Mega Drive - Genesis | (sha1 / filename)
 Sega - Master System - Mark III | (sha1 / filename)
 Sega - Game Gear | (sha1 / filename)

## Sources des boxart
* [GameTDB](https://gametdb.com) avec correspondance par titleid.
* [LibRetro](https://github.com/libretro/libretro-thumbnails) avec correspondance par sha1 basée sur [NoIntro](https://datomatic.no-intro.org)  ou simplement par nom de fichier. [LibRetro DAT](https://github.com/libretro/libretro-database/tree/master/dat) est actuellement ajouté comme source supplémentaire pour le sha1 des jeux NES.

## Téléchargement
Pas disponible pour l'instant
