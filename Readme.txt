Student Details : 
	1) Manan Rajpal (100864824)
	2)James
	//Please add Group number from ESP

Features/Bugs/Missing
	Features:
		1) User shall be able to test if q is entailed from KB.
		2) User can access the program with the help of a bash file.
		3) Forward Chaining method helps the user to find the query from the given facts.
			-The way forward chaining works is by reading the input file which contains a question, clauses and facts.
			-The program intelligently splits the content of the file between facts, clauses and question.
			-The facts are stored in facts list, clauses in clauses list and question as question variable.
			-To incorporate the flow of program lists such as agenda, inferred and count are created.
			-Agenda stores the symbol that need to be processed and check against premises of clauses checking if they match.
			-Inferred is the list that contains symbols which are popped from agenda so that we can track the flow to the
			 result and avoid duplication.
			-Count is the list that stores the integer which represents the number of symbols within a given premise of clause,
			 let's say p & q => r has two count value as there are two symbols in the premise. This list helps to make sure 
			 that when all the symbols in the premise are processed we can store the result of clause in the agenda and process
			 it.
			-The program starts by initiating all the values with their respective values and taking each fact and matching 
			 it with any of the premise of clauses, if it does it stores the result of clause in agenda and conitnues until
			 symbol matching the question is achieved or stored in agenda.

		4) Backward Chaining can be used to prove query by backtracking through given clauses to reach the fact.
			-The way forward chaining works is by reading the input file which contains a question, clauses and facts.
			-The program intelligently splits the content of the file between facts, clauses and question.
			-The facts are stored in facts list, clauses in clauses list and question as question variable.
			-To incorporate the flow of program lists such as agenda, inferred and count are created.
			-Agenda stores the symbol that need to be processed and check against premises of clauses checking if they match.
			-Inferred is the list that contains symbols which are popped from agenda so that we can track the flow to the
			 result and avoid duplication.
			-The program in backward chaining flows by initiating lists with their respective values and instead of 
			 starting from fact it now starts with question and starts matching the result of each clause with the question,
			 if it matches than the symbols in premise are added to the agenda but before adding them it is made sure that 
			 they are already not in agenda and have not been processed before. The process keeps on repeating for each clause
			 until a fact is added to agenda which makes the whole clauses true as facts are true hence acheiving the
			 objective.
	Bugs:
	Missing:

Test Cases:

Acknowledgement/Resources:
	1) https://www.youtube.com/watch?v=EZJs6w2YFRM
		-This link was used to understand what is the basic difference between forward and backward chaining.
		-It r-iterated through points which should be kept in mind when designing code of the methods like
			- Making sure to avoid duplicates in backward chaining.
			- Avoid infinite loops.
	2) AI Textbook 3rd edition page 258
		- Textbook was referred for the pseudocode of forward chaining and to understand how the program should flow and
		  what things to avoid.
	3) http://snipplr.com/view/56297/
		- As there was not much information available about pseudocode for backward chaining, this link was used to understand
		  how the program flows for backward chaining and what things are to be kept in mind for it to work properly.

Notes:

Summary Report:
	The team comprised of two members James and Manan who divided the responsibility among themselves.
		- James
			- Created basic skeleton of the program.
			- Added code for reading content from the file.
			- Worked on the read me file and gave input.
			- Implemented the whole truth table method.
		- Manan	
			- Modified some code in the basic skeleton of how the output is returned to the main class and shown on console.
			- Implemented the whole Forward Chaining.
			- Implemented the whole Backward Chaining.
			- Created half of the readme file.

	Both members regulary met in person to discuss the update made by each and to understand if someone is going the wrong way.
	To communicated out of regular hours, facebook was used to update and ask for help if stuck on any part of the program where both
	readily helped each other throughout the implication of the assignment. 