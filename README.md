# Bienvenue Sur notre projet MowGame !

Il s'agit d'un projet développé sous-visual studio 2015.

L'objectif est de reproduire le jeu de carte MOW à l'aide du Framework WPF.
Il devrait permettre à un ou plusieurs joueurs humains de s'opposer entre eux ou face à des IA.

# Le design

* Résolution: 1024x768
* Plateau de jeu dans la partie haute de l'écran, joueurs positionnés en cercle autour du troupeau et de la pioche.
* Les positions ne changent pas au fil de la partie
* Affichage du sens de jeu: icone flèche horaire/anti-horaire
* Dans le troupeau, chaque carte a sa position (statique). 
  * si on peut, on essaiera de centrer les cartes avec positionnement dynamique.

# Le déroulement du jeu

* Actions possibles joueur courant: ramasser le troupeau, jouer carte
* Si impossible de jouer: ramasser
* Si jouer carte retardataire && plusieurs positions disponibles: choix position
* Si carte jouée est spéciale (5 mouches): choix disponible changer de sens de jeu, conserver sens de jeu
* Si aucune pioche et aucun troupeau: fin du tour

# Explications des règles du jeu

## Introduction

Joueurs : 1 à 5, dont au moins 1 joueur humain (jusqu'à 5 joueurs humains, maximum 4 joueurs IA).

Objectif principal: accumuler le moins de points possible au cours des manches du jeu.

Lorsqu'un joueur atteint 100 points, le jeu prend fin. Celui qui a le moins de points gagne.

Les joueurs disposent d'une main de 5 cartes. Ils doivent se débarasser de leurs cartes dans le troupeau (cartes posées ar les joueurs sur la table). Lorsqu'un joueur ne peut ni piocher ni jouer, il ramasse le troupeau.
Les cartes réprésentent des vaches et peuvent avoir de 0 à 5 mouches. C'est le nombre de mouches récupérées par le joueur qui détermine son score.

## Les cartes du jeu

* 15 cartes vaches (numérotées de 1 à 15), avec 0 mouche
* 13 cartes vaches (numérotées de 2 à 14), avec 1 mouche
* 11 cartes vaches (numérotées de 3 à 13), avec 2 mouches
* 3 cartes vaches (numérotées 7, 8, 9), avec 3 mouches
* 6 cartes vaches spéciales, avec 5 mouches

## Les cartes spéciales

* 0 et 16, "les vaches serre file". Elles bloquent chacune une extrémité du troupeau.
* 7 et 9, "les vaches acrobates". Elles se placent par dessus une vache portant le même numéro.
* "Les vaches retardataires" (2 cartes identiques). Elles s'insère entre 2 cartes dont la valeur est au moins éloignée de 2
(par ex: entre 8 et 10, ou encore 5 et 9, mais pas entre 4 et 5, ni entre 14 et 15)

Lorsqu'un joueur utilise une carte spéciale, il peut décider de change le sens de jeu.

## Démarrer une partie

Le jeu se déroule en plusieurs manches.

On commence une manche en mélangeant les 48 cartes et en distribuant 5 cartes à chaque fermier.
Les cartes restantes constituent la pioche, d'où les joueurs tireront leurs cartes.

Le premier fermier choisit le sens de jeu. Il place une carte de sa main dans le troupeau (face visible) et pioche une nouvelle carte.

Par la suite, chaque joueur peut:
* Choisir de ramasser le troupeau ou non
* Puis poser une carte dans le troupeau et en reprendre une dans la pioche.

Si un joueur ne peut pas ajouter de carte dans le troupeau, il doit le ramasser obligatoirement.

Les cartes doivent être disposées de manière à respecter l'ordre de 0 à 16 et il ne peut y avoir qu'une seule carte de chaque numéro dans le troupeau. Seules les 2 vaches acrobates font exception.

La manche prend fin lorsqu'il n'y a plus de pioche et qu'un joueur à ramassé le troupeau.

1. Chaque joueur ajoute sa main à son étable.
2. On compte alors le nombre de mouche que chaque joueur possède dans son étable. 
3. Chaque mouche ajoutant 1 point de honte, on en déduit le score de chaque joueur.
4. On regroupe toutes les cartes et on les mélange.
5. On passe à la manche suivante.

Le jeu s'achève lorsqu'un joueur totalise 100 points de honte.

