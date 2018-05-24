Student Details : 
	1) Manan Rajpal (100864824)
	2)James Goodricke (101082494)

Team Number: COS30019_A02_T020

Features:
	1) User shall be able to test if alpha is entailed in the knowledge base.
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
		 let's say p & q =>  r has two count value as there are two symbols in the premise. This list helps to make sure 
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
	
	5) The Truth Table method applies a brute-force algorithm to find the total number of “worlds” (represented as true rows in the truth table) that entail the question. If a row doesn’t entail a question, then it will return false. Otherwise, it will return the total number of entailed worlds.
		- It does so by dynamically generating each row of the truth table as an array of boolean values.
		- These values are used to test the facts and clauses in the knowledge base to determine if the current row is entailed.
		- If the current row is entailed, it then check if the question (alpha) is true on that row of the knowledge base. If it is true, it's true for all rows of the truth table that fit the knowledge base, then alpha is entailed.

Bugs:
	- The truth table is not able to process knowledge bases with more than 61 literals in them. This is due to the limitations of the int64 datatype and the way the truth table rows are generated. Essentially int64 cannot store numbers larger than 2^61, which is the equation used to generate the total number of columns in the table. Our testing was unable to find an upper limit on literals for forward and backward chaining, (we tested up to 5000 literals).

Test Cases:
	- Test Case 1:
		TELL
		p2=>  p3; p3 =>  p1; c =>  e; b&e =>  f; f&g =>  h; p1=> d; p1&p3 =>  c; a; b; p2;
		ASK
		d
	
	- Test Case 2:
		TELL
		p1&p2&p3=>  p4; p5&p6 =>  p4; p1 =>  p2; p1&p2 =>  p3; p5&p7 =>  p6; p1; p4;
		ASK
		p7
	
	- Test Case 3: 
		TELL
		a&b =>  c; d&e =>  b; a; b;
		ASK
		e
	
	- Test Case 4: 
		TELL
		a; b; c;
		ASK
		a
	
	- Test Case 5: 
		TELL
		a; b; c;
		ASK
		d
	
	- Test Case 6: 
		TELL
		a =>  b; a =>  c; c =>  b;
		ASK
		b
	
	- Test Case 7: 
		TELL
		A1 => A2; A2 => A3; A3 => A4; A4 => A5; A5 => A6; A6 => A7; A7 => A8; A8 => A9; A9 => A10; A10 => A11; A11 => A12; A12 => A13; A13 => A14; A14 => A15; A15 => A16; A16 => A17; A17 => A18; A18 => A19; A19 => A20; A20 => A21; A21 => A22; A22 => A23; A23 => A24; A24 => A25; A25 => A26; A26 => A27; A27 => A28; A28 => A29; A29 => A30; A30 => A31; A31 => A32; A32 => A33; A33 => A34; A34 => A35; A35 => A36; A36 => A37; A37 => A38; A38 => A39; A39 => A40; A40 => A41; A41 => A42; A42 => A43; A43 => A44; A44 => A45; A45 => A46; A46 => A47; A47 => A48; A48 => A49; A49 => A50; A50 => A51; A51 => A52; A52 => A53; A53 => A54; A54 => A55; A55 => A56; A56 => A57; A57 => A58; A58 => A59; A59 => A60; A60 => A61; A61 => A62; A62 => A63; A63 => A64; A64 => A65; A65 => A66; A66 => A67; A67 => A68; A68 => A69; A69 => A70; A70 => A71; A71 => A72; A72 => A73; A73 => A74; A74 => A75; A75 => A76; A76 => A77; A77 => A78; A78 => A79; A79 => A80; A80 => A81; A81 => A82; A82 => A83; A83 => A84; A84 => A85; A85 => A86; A86 => A87; A87 => A88; A88 => A89; A89 => A90; A90 => A91; A91 => A92; A92 => A93; A93 => A94; A94 => A95; A95 => A96; A96 => A97; A97 => A98; A98 => A99; A99 => A100; A1;
		ASK
		A100
	
	- Test Case 8:
		TELL

		ASK
		A

Acknowledgment/Resources:
	1) https://www.youtube.com/watch?v=EZJs6w2YFRM
		-This link was used to understand what is the basic difference between forward and backward chaining.
		-It r-iterated through points which should be kept in mind when designing code of the methods like
			- Making sure to avoid duplicates in backward chaining.
			- Avoid infinite loops.
	2) AI Textbook 3rd edition page 258
		- Textbook was referred for the pseudocode of forward chaining and truth table, as well as how to understand how the program should flow and what things to avoid.
	3) http://snipplr.com/view/56297/
		- As there was not much information available about pseudocode for backward chaining, this link was used to understand
		  how the program flows for backward chaining and what things are to be kept in mind for it to work properly.
	4) Our tutor, Samuel Pinkus, who provided a great deal of help with the design and implementation of the Truth Table Inference Engine. 

Summary Report:
	- The team comprised of two members, James and Manan, who divided the responsibility among themselves.
		- James(50%)
			- Designed the program structure 
			- Implemented the inference engine and program classes.
			- Implemented the truth table class
			- Did some work on the readme file.
			- Performed Testing.
		- Manan(50%)
			- Modified some code in the basic skeleton of how the output is returned to the main class and shown on console.
			- Implemented the Forward Chaining.
			- Implemented the Backward Chaining.
			- Wrote most of the readme file.

	- Both members regularly met in person to discuss the update made by each and to understand if someone is going the wrong way.
	To communicated out of regular hours, Facebook was used to update and ask for help if stuck on any part of the program where both
	readily helped each other throughout the implication of the assignment.
