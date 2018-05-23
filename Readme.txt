Student Details : 
	1) Manan Rajpal (100864824)
	2)James Goodricke (101082494)

Team Number: COS30019_A02_T020

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
		5. The Truth Table method applies a brute-force algorithm to find the total number of “worlds” (represented as true rows in the truth table) that entail the question. If a row doesn’t entail a question, then it will return false. Otherwise, it will return the total number of entailed worlds.
	Bugs:
		- The truth table is not able to process knowledge bases with more than 30 literals in them. This is due to the limitations of the int32 datatype and the way the truth table rows are generated (essentially int32 cannot store numbers larger than 2^30, which is the equation used to generate the total number of columns in the table). Our testing was unable to find the upper limit of literals for forward and backward chaining (upwards of 5000 literals).
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
			- Designed the program structure 
			- Implemented the inference engine and program classes.
			- Implemented the truth table class
			- Worked on the read me file and gave input.
		- Manan	
			- Modified some code in the basic skeleton of how the output is returned to the main class and shown on console.
			- Implemented the Forward Chaining.
			- Implemented the Backward Chaining.
			- Created most of the readme file.

	Both members regulary met in person to discuss the update made by each and to understand if someone is going the wrong way.
	To communicated out of regular hours, facebook was used to update and ask for help if stuck on any part of the program where both
	readily helped each other throughout the implication of the assignment.
