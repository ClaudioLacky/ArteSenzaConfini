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

	$name = $_POST["nome"];
	$surname = $_POST["cognome"];
	$userEmail = $_POST["mail"];
	$userPassword = $_POST["password"];
	$nomeutente = $_POST["nomeUtente"];

	// Querying the database to check if the users email is already in the db
	$emailCheckQuery = "SELECT email FROM persone WHERE email='" . $userEmail . "';";
	
	$emailCheck = mysqli_query($conn, $emailCheckQuery) or die("2: Email check failed"); // error code #2 - email check query failed
	
	if(mysqli_num_rows($emailCheck)>0)
	{
		echo "3: Email already exists"; // error code #3 - email exists cannot register
		exit();
	}
	
	// Add user to the table
	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $userEmail . "\$";
	$hash = crypt($userPassword, $salt);
	
	$insertUserQuery = "INSERT INTO persone (nome, cognome, email, password, nomeUtente, salt) VALUES ('" . $name . "', '" . $surname . "', '" . $userEmail . "', '" . $hash . "', '" . $nomeutente . "', '" . $salt . "');";
	
	mysqli_query($conn, $insertUserQuery) or die("4: Insert user query failed"); // error code #4 - insert query failed
	
	// Success
	echo("success");
	$conn->close();
?>