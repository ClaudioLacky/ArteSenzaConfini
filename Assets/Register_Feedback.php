<?php

	header("Access-Control-Allow-Credentials: true");
	header('Access-Control-Allow-Origin: *');
	header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
	header('Access-Control-Allow-Headers: Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time');

	$servername = "localhost";
	$DBusername = "matteo";
	$DBpassword = "root";
	$DBname = "galleria_arte";
	
	// Create Connection to database
	$conn = mysqli_connect($servername, $DBusername, $DBpassword, $DBname);

	// Check connection
    if (mysqli_connect_errno())
	{
        echo "1: Connection failed"; // error code #1 = connenction failed
		exit();
    }
	
	$nomeUtente = $_POST["nomeUtente"];
	$feedback= $_POST["feedback"];
	
	// Querying the database to check if the username is already in the db
	$userCheckQuery = "SELECT id FROM persone WHERE nomeUtente='" . $nomeUtente . "';";
	
	$userCheck = mysqli_query($conn, $userCheckQuery) or die("2: User check failed"); // error code #2 - user check query failed
	
	if(mysqli_num_rows($userCheck) != 1)
	{
		echo "5: Either no user with username, or more that one"; // error code #5 - number of username matching != 1
		exit();
	}
	
	// Get user info from query
	$existinginfo = mysqli_fetch_assoc($userCheck);
	$id_persona = $existinginfo["id"];
	
	// Add feedback to the table
	$insertFeedbackQuery = "INSERT INTO commenti (feedback, id_persona) VALUES ('" . $feedback . "', '" . $id_persona . "');";
	
	mysqli_query($conn, $insertFeedbackQuery) or die("4: Insert feedback query failed"); // error code #4 - insert query failed
	
	//Success
	echo("success");
	$conn->close();
?>