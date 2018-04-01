<?php
$servername = "csdb.wheaton.edu";
$username = "pocketwhalesadm";
$password = "qh0UnB1T2R6v";
$dbName = "pocketwhales";


$conn = new mysqli($servername, $username, $password, $dbName);

if(!$conn){
	die("Connection Failed " . mysqli_connect_error());
}


$ID = $_GET["ID"];
$sql = "SELECT ID, Whale, Skin, value FROM items";
$result = mysqli_query($conn, $sql);

if($ID != ""){
	$sql2 = "SELECT * FROM items where ID=" . $ID;
	$id = mysqli_query($conn, $sql2);


	if(mysqli_num_rows($id) > 0){
		$row =  mysqli_fetch_assoc($id);
		echo "|ID:" . $row['ID'] . "|Whale:" . $row['Whale'] . "|Skin:" . $row['Skin']. "|value:" . $row['value']. ";";
	}
}
if(mysqli_num_rows($result) > 0){
	while($row = mysqli_fetch_assoc($result)){
		echo "|ID:" . $row['ID'] . "|Whale:" . $row['Whale'] . "|Skin:" . $row['Skin']. "|value:" . $row['value']. ";";
	}
}


?>
