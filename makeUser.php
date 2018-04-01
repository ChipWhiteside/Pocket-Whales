<?php
	

$servername = "csdb.wheaton.edu";
$dbusername = "pocketwhalesuser";
$dbpassword = "2iW35MTgOv9b";
$dbName = "pocketwhales";

$username = $_POST["username"];
$password = $_POST["password"];
$date_created = date("Y-m-d h:i:sa");
$date_modified = date("Y-m-d h:i:sa");

$conn = new mysqli($servername, $dbusername, $dbpassword, $dbName);

if(!$conn){
	die("Connection Failed " . mysqli_connect_error());
}




$sql = "INSERT INTO users (username, password, date_created, date_modified) VALUES ('".$username ."','".$password."','". $date_created."','".$date_modified."')";


$user = mysqli_query($conn, $sql);
echo $user;

if(!$user){
	echo "User name not available";
}else echo "no probs";



 


?>
