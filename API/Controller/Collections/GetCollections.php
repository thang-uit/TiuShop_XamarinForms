<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Collections.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$collections = new Collections($db->connect());

$arrayCollections = $collections->getCollections();

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayCollections,
    "message" => "Get Collections"
);

echo json_encode($array);
