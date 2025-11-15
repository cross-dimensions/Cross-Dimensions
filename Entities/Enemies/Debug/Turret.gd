class_name Turret extends Node2D

@onready var turretAxis = $TurretAxis

var player

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	player = get_tree().get_first_node_in_group("Player")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	# Get players angle relative to turret axis
	var axisAngle = self.get_angle_to(player.global_position) - deg_to_rad(90)
	
	# Constrain the rotation of the axis
	axisAngle = clamp(axisAngle, deg_to_rad(-60), deg_to_rad(60))
	
	# Set rotation
	turretAxis.set_rotation(axisAngle)
