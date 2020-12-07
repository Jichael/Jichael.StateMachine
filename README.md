Ce package est une implémentation du modèle de Finite State Machine (FSM) (https://fr.wikipedia.org/wiki/Automate_fini).

Dans le contexte de nos applications, il peut servir par exemple pour la mise en place de scénario de jeu.

Il est composé de :

    - Un script "StateMachineManager" qui va gérer le déroulement d'un scénario "StateMachine"
    - Une StateMachine est un graphe orienté, dont les noeuds sont les états "State" et les arcs des transitions "Transition".
    - Une transition possède 1 à N conditions "TransitionCondition", qui doivent toutes être validées pour que la transition soit réalisée.
    - La classe TransitionCondition est une classe abstraite qui peut être héritée pour créer toute sorte de condition requise par le scénario.
    - On peut associer à chaque état et à chaque transition des événements qui vont influer sur l'application.


Pour construire un scénario dans l'éditeur :

 1) Le script StateMachineManager doit être présent dans la scène
 1-opt) Sur le script StateMachineManager, on peut renseigner une StateMachine et cocher "AutoStart" pour débuter le scénario automatiquement.
 
 2) On crée un scénario avec le script StateMachine. On peut lui donner un nom, et on renseigne l'état de départ.
 
 3) On crée les états et leurs transitions : un gameObject avec le script State = 1 état.
 
 4) Si l'état n'est pas final (au moins un arc vers un autre état), alors on crée un gameobject en enfant de cet état pour chaque arc.
 
 5) Dans la transition, on renseigne l'état d'arrivé, eventuellement les évenements à executer lors de la transition, et on rajoute les scripts de conditions que l'on souhaite (commencant par "TC").
 
   - TCActiveState : Condition validée si le gameobject est dans l'état de "wantedState"
   - TCControlled/State : Condition validée si le booleen State est dans l'état "wantedState"
   - TCKeyTrigger : Condition validée si la touche est enfoncée
   - TCStatePathError : Condition validée si l'état est présent au moins "minNumForError" fois dans le chemin pris par le joueur.
   - TCTimer : Condition validée après X secondes
   - TCTransformDistance : Condition validée si les deux transform sont à plus/moins (selon checkIfWithin) de "distance" unités
   - etc...

 6) Pour les états finaux, on coche la case EndState sur le script de l'état et on supprime les conditons en enfant s'il y en a.
 
 
Un exemple de scénario est disponible dans le dossier Samples/ du package.