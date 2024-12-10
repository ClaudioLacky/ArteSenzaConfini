<?php
	$servername = "localhost";
	$DBusername = "domenico";
	$DBpassword = "root";
	$DBname = "galleria_arte";
	
	// Create Connection to database
	$conn = new mysqli($servername, $DBusername, $DBpassword; $DBname);
	
	if ($conn->connect_error)
	{
        die("1"); // 1 = connenction to database failed
    }
	
	$nomeutente = $_POST["nomeUtente"];
	$descrizione= $_POST["Descrizione"];
	
	$querynomeUtente = "SELECT id FROM persone WHERE nomeUtente='".$nomeutente."';";
	
	
	$id = mysqli_query($conn, $querynomeUtente) or die("4: Insert user query failed"); // error code #4 - insert query failed
	
	$insertCommentQuery = "INSERT INTO persone (descrizione,id_persona) VALUES ('" . $descrizione . "', '" . $id . "');";
	
	mysqli_query($conn, $insertCommentQuery) or die("4: Insert user query failed"); // error code #4 - insert query failed
	
	echo("success");
	$conn->close();
?>