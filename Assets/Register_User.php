<?php
	$servername = "localhost";
	$DBusername = "domenico";
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
	$emailCheckQuery = "SELECT mail FROM persone WHERE mail='" . $userEmail . "';";
	
	$emailCheck = mysqli_query($conn, $emailCheckQuery) or die("2: Email check failed"); // error code #2 - email check query failed
	
	if(mysqli_num_rows($emailCheck)>0)
	{
		echo "3: Email already exists"; // error code #3 - email exists cannot register
		exit();
	}
	
	// Add user to the table
	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $name . "\$";
	$hash = crypt($userPassword, $salt);
	
	$insertUserQuery = "INSERT INTO persone (nome, cognome, mail, password, nomeUtente) VALUES ('" . $name . "', '" . $surname . "', '" . $userEmail . "', '" . $hash . "','" . $nomeutente . "');";
	
	mysqli_query($conn, $insertUserQuery) or die("4: Insert user query failed"); // error code #4 - insert query failed
	
	// Success
	echo("success");
	$conn->close();
?>