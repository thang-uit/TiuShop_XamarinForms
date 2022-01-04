<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Slider.php');
include_once('../Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$slider = new Slider($db->connect());

$amount = isset($_GET["amount"]) && !empty($_GET["amount"]) ? $_GET["amount"] : "5";

$arraySlider = $slider->getSlider($amount);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arraySlider,
    "message" => "Slider"
);

echo json_encode($array);
