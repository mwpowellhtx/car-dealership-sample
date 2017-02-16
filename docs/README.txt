
11:25 PM 2/10/2017

Progress thus far; will adjust as discoveries are made:

* most of the domain model is fleshed out;

* data layer migrations are set and working; could work on transactional unit of work material, but this will work for now...

next up:

* work on repository data layers,

* decompose service and controller layers,

* view and view model layers,

* may require third party like Autofac (or other DI), Automapper (for convenient DTO translations)

bonus round:

* decide whether and/or what sort of serialization is required, i.e. for wizardly navigation...


1:23 PM 2/14/2017

after numerous updates...

remember to add the following links:

Year/
Manufacturer/
TODO: Model/

let's just fast forward to a more interesting controller: ModelYear

ModelYear/
Vehicle/


9:40 PM 2/15/2017

Could spend a little time adding a side bar and/or menu strategy.

For interview purposes, the following pattern is used to demo the features in browser:

<uri/>/Year
<uri/>/Manufacturer
<uri/>/ModelYear
<uri/>/Vehicle
