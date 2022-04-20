<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class chuck_talks extends Model
{
    use HasFactory;

    protected $table = 'chuck_talks';

    protected $primaryKey = 'id';

    public $incrementing = true;

    protected $keyType = 'integer';

    public $timestamps = false;

    protected $fillable = [
        'frase',
        'accion'
    ];
}
