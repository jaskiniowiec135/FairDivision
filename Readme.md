
# Table of contents
1. [Introduction](#Introduction)
2. [Master degree](#Master-degree)
3. [User instruction](#User-instruction)
4. [Application](#Application)
5. [Further plans](#Further-plans)

# Introduction

This repository stores a project, which was a technical part of my master's degree thesis.

# Master degree

The fair division is a matter of many theoretical articles as well as the topic of much research. It is widely mentioned in real-life problems, like kidney donation or students assignments to high schools. Those problems are related to resources, like organs or places for students, and as we know most of the resources are limited. That creates a need of finding a way of optimal, or at least as close as it can be to optimal resource allocation. Thanks to computer science which is still growing and expanding to every part of our lives, scientists were able to prepare algorithms and use computers to make some processes automated and fast. 

## TTC algorithm

One of the examples of a solution for fair allocation of resources was an algorithm proposed by David Gale at the turn of the 60s and 70s of the 20th century, which was able to divide resources between participants of the division without involving money, according to their preferences, which were set by the doctor. It is also important to mention, that the TTC algorithm was strategy-proof, which means that the user wouldn't get better resources if he pass false preferences. Moreover, this algorithm also meets the conditions of Pareto efficiency, because after finding a cycle of trade there is no possibility to find a better solution with the same participants and resources. The algorithm steps are listed below:

* Honor donors are signed to the system (in some cases, families of the waiting patients also signed in to exchange their kidney for their relative)
* Every participant introduces his preferences (in kidney example it was done by the doctor) to the system
* System proceeds through all cases and looks for cycles
* Founded cycle is removed from the system, and chosen patients are prepared for the procedure
* If there are any patients left, the algorithm proceeds once more

After some struggles with the authorities and aversion from the medical community, he was able to try his algorithm in real life. Over the next years thanks to the TTC (Top trading cycle) algorithm, the number of kidney donations, as well as successful transplantations grew up.

## YRMH-IGYT

Gale wasn't the only scientist working on the fair division problem. Other worth mentioning scientists who worked on this problem are Atila Abdulkadiroglu and Tayfun Sönmez, who looked at the same problem from a different perspective. As an example, they analyze the hypothetical situation in the dormitory, where students have their rooms. They published the result of their work in 1999, presenting YRMH-IGYT (You request my home - I get your turn) algorithm. At the start of every academic year new students arrive and want to get a room for themselves, but there are also students which already got a room, but want to change it according to their preferences. This time approach to the division was different because inside TTC might be a situation, when a patient is allowed to take only one specific kidney, here however every participant determines its preferences from the most valuable item to the least one. What is also important is that every participant can only choose once, which reduces the maximum number of cycles as well as the risk of infinite cycles. YRMH-IGYT algorithm also meets the conditions of Pareto efficiency. The algorithm steps are listed below:

* Participants give their preferences according to the free rooms from the best one to the worst one
* We are setting random order of the choosing participants
* Participants choose and take their best rooms as long as the best choice of the participant is already taken by another participant, who didn't choose yet. In this situation occupant of the desired room have an opportunity to get his best room. The situation may be repeated until it will create a cycle or a line. In case of line, the last participant wants to choose the free room, take it and release the currently held room, which can be taken by the previous participant. In the case of the cycle, the first participant agrees to release his room for the last one in the cycle, which finally gives him the room he wanted at the beginning.
* If the participant is not able to take his best choice, because it's already taken by another participant, who took it during the run of this algorithm, it simply takes the next one from the preference list. Participant does this as long as there are free rooms because he has to set preferences for all of the rooms. This requires us to prepare at least an equal number of rooms and participants.
* At the end, all of the participants have their room.

The advantage of this algorithm is that the participant can choose his room, but only if it is the best choice for this participant.

## YRMH-IGYT modification

This project changes the way of preparing preference lists for participants. In the classic version of the algorithm, participants had to explicitly set the ranking of the available objects. However, we can get the list of desired objects according to their attributes. In the AppData is stored an example, where we can check how this application can be used. According to the mentioned example, we can set crucial attributes for the group of the objects, and let specific users express their preferences for the ideal object. Then we can enter attributes of the real-life objects along with the owners if part of the objects is already occupied. After some preparation, we are ready to process the algorithm and get the list of the assignments participants to the objects.

# User instruction

In its current form application is delivered as a WPF project. It contains four tabs, where the user can prepare data and process the algorithm. 

## Configuration

### Possible operations

In this tab, user can:
* can create new configuration *Save configuration* 
* get already saved configuration *Get configuration*
* remove existing configuration *Remove configuration*

### Usage example

In the combobox user can insert the name of the new configuration or choose the existing one. After setting the name for the new configuration user must insert attributes. Depending on the type of objects divided between persons, user have to decide which attributes are crucial and most important for them. In the given example (ExampleConfiguration) houses are the objects of division, and attributes distinguishing them are:

* number of bathrooms
* number of swimming pools
* size of the garden
* number of the garages
* price

What is worth mentioning, in the classic approach to the YRMH-IGYT algorithm, there was no possibility of talking about the price of the objects. However here it can be one of the attributes of the object. After passing all attributes and their units user has to save them with the "Save configuration" button. If it was a new configuration, the specific folder is created, otherwise, the existing file is overwritten with the currently inserted value. Please keep in mind, that user don't have to pass all attributes, but every attribute must be passed as pair of name and unit, otherwise, it will be ignored. For now, the user can pass a maximum of 5 attributes, however, in the future, there are plans for a dynamic number of attributes to add. After preparing the configuration user can go to the next tab named *Members*. If the user already prepared the configuration earlier, he can also go to the next tab or select configuration from the list and click the *Get configuration* button to verify attributes. However, if the user wants to remove an already prepared and saved configuration, he must choose one from the list and click the *Remove configuration* button.

## Members

### Possible operations

In this tab, the user can:
* save a member *Save member*
* remove a member from the list *Remove member*
* save all members on the list to the file *Save members*
* remove file with saved members *Remove members*

### Usage example

At the very beginning, the user has to choose a configuration from the dropdown list. After that user can enter specific participants of the division along with their preferences for the ideal object. For every attribute, there are 3 cells to input data. The first of them is for the best value of the specific attribute, the second one is for the acceptable value, and the last one is for the rank of the specific attribute. This approach is for users, which don't care about anything except one or two things, like Grzesiek, who is only looking at the number of bathrooms, the number of garages, and price. It doesn't mean that the ideal object for Grzesiek is without a garden and swimming pool, however, their number doesn't influence the final rate of the specific object. The final rate for the specific object is calculated as a sum of rates of all attributes, which are counted according to the following pattern:

* if best value is bigger than accepted value - ((actualValue + 1.0) / (bestValue + 1.0)) * rank
* if best value is smaller than accepted value - ((bestValue + 1.0) / (actualValue + 1.0)) * rank

Thanks to the + 1.0 operation we are covering the situation when the best value is 0, but as long as the ranks are related only between themselves, it doesn't affect the final result of the algorithm process. This is why rank is so important thing because there is no need of setting ranks to sum them to 1.0, it might as well be 100 or 350. For consistency and ease of entering the data by the user, all of the participants should have the same amount of points to assign to the ranks, but it's also not necessary, because ranking is counted separately for every participant and object, so rank limit equal to 1000000 at one participant doesn't affect to the rank limit 1.0 at the other participant. The only thing which is important at rank values is that they are related in the scope of one member, so if the first attribute has rank 2, and the second has rank 1, the first attribute is twice more important for this particular member than the second attribute.

After passing all attributes for a single participant user have to click *Save member* button, which adds this member to the temporary list. If the user wants to remove a specific member from the list, he must select this user and click the *Remove member* button. After passing all of the members user have to click *Save members* button, which creates a file from the members list. If the user wants to remove this file, he needs to select the specific configuration from the dropdown list and click the *Remove members* button. After saving members to the file user can go to the *Objects* tab.

## Objects

### Possible operations

In this tab, the user can:
* save single object *Save object*
* remove a single object from the list *Remove object*
* save all objects on the list to the file *Save objects*
* remove file with saved objects *Remove objects*

### Usage example

As in the previous tab, the user must select the already prepared configuration from the dropdown list. Now the user can insert object name and attributes, as well as select the current owner of this object from the dropdown list if the object is already occupied by someone.

After passing all attributes values user can save the currently chosen object by clicking the *Save object* button. If the user wants to remove the currently chosen object, he has to click the *Remove object* button. If all of the objects are passed correctly, the user can save these objects to the file by clicking the *Save objects* button. If the user wants to remove an already prepared file with objects, he has to select a specific configuration and click the *Remove objects* button.

# Application

For now, the application is divided into two projects, where one of them is the core project with all logic related to the algorithm process, and the second one is created in WPF technology, which utilizes the algorithm mechanism.

# Further plans

For now, I will focus on unit tests for the algorithm because for now all changes were tested manually. After covering the project with tests I will add another project which will utilize algorithm via WebAPI technology and Swagger interface. The next step will be publishing algorithm code in form of a NuGet package and utilizing it in the application via package.

