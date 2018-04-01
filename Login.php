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




$sql = "SELECT password FROM users WHERE username='" .$username."'";


$result = mysqli_query($conn, $sql);

if(mysqli_num_rows($result) > 0){
	while($rtpassword = mysqli_fetch_assoc($result)){
	if($rtpassword['password'] == $password){
		echo "gooooooood!!" ;
	}else
		echo "Death LOOMS, wrong pass";
	}
}else
	echo "username not found!!!1"; 



 


?>
