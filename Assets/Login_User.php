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
	
	$userEmail = $_POST["mail"];
	$userPassword = $_POST["passwordLogin"];
	
	// Querying the database to check if the users email is already in the db
	$emailCheckQuery = "SELECT email, password, nomeUtente, salt FROM persone WHERE email='" . $userEmail . "';";
	
	$emailCheck = mysqli_query($conn, $emailCheckQuery) or die("2: Email check failed"); // error code #2 - email check query failed
	
	if(mysqli_num_rows($emailCheck) != 1)
	{
		echo "5: Either no user with email, or more that one"; // error code #5 - number of emails matching != 1
		exit();
	}
	
	// Get login info from query
	$existinginfo = mysqli_fetch_assoc($emailCheck);
	$email = $existinginfo["email"];
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["password"];
	
	$loginhash = crypt($userPassword, $salt);
	
	if ($hash != $loginhash)
	{
		echo "6: Incorrect password"; // error code #6 - password does not hash to match table
		exit();
	}
	else
	{
		echo("success");
		$conn->close();
	}
?>