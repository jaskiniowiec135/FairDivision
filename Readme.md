# Introduction

This repository stores a project, which was a technical part of my master degree thesis.

# Master degree

Fair division is a matter of many theoretical articles as well as topic of many research. It is widely mentioned in real life problems, like kidney donation or students assignment to the high schools. Those problems are related with resources, like organs or places for students, and as we know most of the resources are limited. That creates a need of finding a way of optimal, or at least as close as it can be to optimal resource alocation. Thanks to the computer science which is still groving and expanding to the every part of our lives, scientists were able to prepare algorithms and use computers to make some procesess automated and fast. 

## TTC algorithm

One of the examples of solution for fair alocation of resources was an algorithm proposed by David Gale at the turn of 60s and 70s of the 20th century, which was able to divide resources between participants of the division without involving money, according to their preferences, which were set by the doctor. It is also important to mention, that TTC algorithm was strategy-proof, which means that user wouldn't get better resource if he pass false preferences. Moreover, this algorithm also meets the conditions of Pareto efficiency, because after finding a cycle of trade there is no possibility to find better solution with the same participants and resources. Algorithm steps are listed below:

* Honor donors are signed to the system (in some cases, families of the waiting patients also signed in to exchange their kidney for their relative)
* Every participant introduces his preferences (in kidney example it was done by the doctor) to the system
* System proceed through all cases and looking for cycles
* Founded cycle is removed from the system, and choosen patients are prepared to the procedure
* If there are any patient left, algorithm proceeds once more

After some struggles with the authorities and aversion from the medical community he was able to try his algorithm in real life. Through next years thanks to the TTC (Top trading cycle) algorithm, number of kidney donations, as well as succesfull transplantations grown up.

## YRMH-IGYT

Gale wasn't the only one scientist working on fair division problem. Another worth mentioning scientists who worked on this problem are Atila Abdulkadiroglu and Tayfun Sönmez, who looked at the same problem from different perspective. As an example they analyze hipotetical situation at dormitory, where students have their own rooms. They published result of their work at 1999, presenting YRMH-IGYT (You request my home - I get your turn) algorithm. At the start of every academic year new students arrive and want to get room for themselfes, but there are also students which already got a room, but want to change it according to their preferences. This time approach to the division was different, because at TTC might be a situation, when patient is allowed to take only one specific kidney, here however every participant determines its preferences from the most valuable item to the least one. What is also important is that every participant can only choose once, which reduces maximum number of cycles as well as the risk of infinite cycles. YRMH-IGYT algorithm also meets the conditions of Pareto efficiency. Algorithm steps are listed below:

* Participants gives their preferences according to the free rooms from the best one to the worst one
* We are setting random order of the choosing participants
* Participants choose and take their best rooms as long as the best choice of the participant is already taken by other participant, who didn't choose yet. In this situation occupant of the desired room have an opportunity to get his best room. Situation may be repeating until it will create a cycle, or a line. In case of line, last participant want to choose free room, take it and release currently holded room, which can be taken by previous participant. In case of cycle, first participant agrees to release his room for the last one in the cycle, which finally gives him the room he wanted at the beggining.
* If participant is not able to take his best choice, because its already taken by other participant, who took it during the run of this algorithm, it simply takes the next one from the preference list. Participant do this as long as there are free rooms, because he has to set preferences for all of the rooms. This requires from us to prepare at least equal number of rooms and participants.
* At the end all of the participants have their own room.

The advantage of this algorithm is that participant can choose his own room, but only if it is the best choice for this participant.

## YRMH-IGYT modification

This project change the way of preparing preference list for participants. In classic version of the algorithm participants had to explicitly set ranking of the available objects. However we are able to get the list of desired objects according to their attributes. In the AppData is stored an example, where we can check how this application can be used. According to the mentioned example, we are able to set crucial parameters for the group of the objects, and let specific users express their preferences to the ideal object. Then we are able to enter parameters of the real life objects along with the owners, if part of the objects are already occupied. After some preparation we are ready to process the algorithm and get the list of the assignments participants to the objects.

# User instruction

In current form application is delivered as WPF project. It contains four tabs, where user can prepare data and process the algorithm. 

## Configuration

At the first tab user can create new configuration, create one or remove existing, already saved to file. In the combobox user can insert name of the new configuration or choose existing one. After setting name for new configuration user must insert parameters. Depending on the type of objects divided between persons, user have to decide which attributes are crucial and most important for them. In given example (ExampleConfiguration) houses are the objects of division, and attributes distinguishing them are:

* number of bathrooms
* number of swimming pools
* size of the garden
* number of the garages
* price

What is worth mentioning, in the classic approach to the YRMH-IGYT algorithm, there were no possibility of talking about the price of the objects. However here it can be one of the parameters of the object. After passing all parameters and their unit user have to save it with "Save configuration" button. If it was new configuration, specific folder was created, otherwise existing file was overwritten with currently inserted value. After preparing configuration user are able to go to the next tab named "Members".

## Members

# Application

For now application is divided into two projects

# Further plans




